using Common.Entities;
using Common.Services.Infrastructure.Repositories.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.Extras
{
    public class ContentStatusesRepository : BaseRepository<ContentStatus, DataContext>, IContentStatusesRepository
    {
        public ContentStatusesRepository(DataContext context) : base(context)
        {
        }

        #region CRUD CATEGORY
        public async Task<ContentCategory> GetCategoryId(int id, ContextSession session)
        {

            try
            {
                var context = GetContext(session);
                var result = await context.ContentCategories.Where(u => u.Id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContentCategory>> GetCategorys(ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.ContentCategories.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ContentCategory> UpdateCategory(ContentCategory contentCategory, ContextSession session)
        {
            try
            {
                var context = GetContext(session);

                var objectExists = await GetEntities(session)
                                         .Where(x => x.Id == contentCategory.Id)
                                         .CountAsync() > 0;

                context.Entry(contentCategory).State = objectExists ? EntityState.Modified : EntityState.Added;

                if (context.Entry(contentCategory).State == EntityState.Added)
                {
                    contentCategory.CreatedAt = DateTime.Now;
                    context.ContentCategories.Add(contentCategory);
                }

                await context.SaveChangesAsync();
                return contentCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteCategory(int id, ContextSession session)
        {
            var context = GetContext(session);
            var ownlist = context.ContentCategories.FirstOrDefault(x => x.Id.Equals(id));
            if (ownlist == null) return false;
            else
                context.ContentCategories.Remove(ownlist);

            return await context.SaveChangesAsync() > 0 ? true : false;
        }
        #endregion

        #region CRUD STATE

        public async Task<ContentStatus> GetStateId(int id, ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.ContentStatuses.Where(u => u.Id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContentStatus>> GetStates(ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.ContentStatuses.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ContentStatus> UpdateState(ContentStatus contentStatus, ContextSession session)
        {
            try
            {
                var context = GetContext(session);

                var objectExists = await Exists(contentStatus, session);

                context.Entry(contentStatus).State = objectExists ? EntityState.Modified : EntityState.Added;

                if (context.Entry(contentStatus).State == EntityState.Added)
                {
                    contentStatus.CreatedAt = DateTime.Now;
                    context.ContentStatuses.Add(contentStatus);
                }

                await context.SaveChangesAsync();
                return contentStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteState(int id, ContextSession session)
        {
            var context = GetContext(session);
            var contentStatus = context.ContentStatuses.FirstOrDefault(x => x.Id.Equals(id));
            if (contentStatus == null) return false;
            else
                context.ContentStatuses.Remove(contentStatus);

            return await context.SaveChangesAsync() > 0 ? true : false;
        }
        #endregion
    }
}