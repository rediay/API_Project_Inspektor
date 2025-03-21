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
    public class ContentTypeRepository : BaseRepository<ContentType, DataContext>, IContentTypeRepository
    {
        public ContentTypeRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedResponseDTO<List<ContentType>>> GetAll(ContextSession session,
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

            var pagedResponseDto = new PagedResponseDTO<List<ContentType>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        #region CRUD TypeContent
        public async Task<List<ContentType>> GetTypeContent(ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.ContentTypes.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ContentType> GetTypeContentId(int id, ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.ContentTypes.Where(u => u.Id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ContentType> UpdateTypeContent(ContentType contentType, ContextSession session)
        {
            try
            {
                var context = GetContext(session);

                var objectExists = await Exists(contentType, session);

                context.Entry(contentType).State = objectExists ? EntityState.Modified : EntityState.Added;

                if (context.Entry(contentType).State == EntityState.Added)
                {
                    contentType.CreatedAt = DateTime.Now;
                    context.ContentTypes.Add(contentType);
                }

                await context.SaveChangesAsync();
                return contentType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteTypeContent(int id, ContextSession session)
        {
            var context = GetContext(session);
            var contentType = context.ContentTypes.FirstOrDefault(x => x.Id.Equals(id));
            if (contentType == null) return false;
            else
                context.ContentTypes.Remove(contentType);

            return await context.SaveChangesAsync() > 0 ? true : false;
        }

        #endregion
    }
}