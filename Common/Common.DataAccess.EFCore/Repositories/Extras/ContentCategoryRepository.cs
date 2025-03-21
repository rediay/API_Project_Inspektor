using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Extras;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.Extras
{
    public class ContentCategoryRepository : BaseRepository<ContentCategory, DataContext>, IContentCategoryRepository 
    {
        public ContentCategoryRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<ContentCategory>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session)
                .Where(obj => obj.Name.Contains(paginationFilterDto.query));

            var newsList = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<ContentCategory>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
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

        public async Task<List<ContentCategory>> GetCategorysbyType(int idtype,ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.ContentCategories.Where(x => x.ContentTypesId == idtype).ToListAsync();
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

                var objectExists = await Exists(contentCategory, session);

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
    }
}