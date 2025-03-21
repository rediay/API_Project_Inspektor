using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Extras;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.Extras
{
    public class ContentRepository : BaseRepository<Content, DataContext>, IContentRepository<Content>
    {
        public ContentRepository(DataContext context) : base(context)
        {
        }

      
        public async Task<PagedResponseDTO<List<Content>>> GetAll(ContextSession session,
            ContentPaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session);

            if (paginationFilterDto.Title is not null && paginationFilterDto.Title != "null")
                queryEntities = queryEntities.Where(obj => obj.Title.Contains(paginationFilterDto.Title));

            if (paginationFilterDto.CountryId is not null)
                queryEntities = queryEntities.Where(obj => obj.CountryId == paginationFilterDto.CountryId);

            if (paginationFilterDto.CategoryId is not null)
                queryEntities = queryEntities.Where(obj => obj.ContentCategoryId == paginationFilterDto.CategoryId);

            if (paginationFilterDto.TypeId is not null)
                queryEntities = queryEntities.Where(obj => obj.ContentTypeId == paginationFilterDto.TypeId);



            var newsList = await queryEntities.Include(x => x.ContentCategory)
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<Content>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }

        public async Task<List<Content>> GetAllNews(ContextSession session, ContentPaginationFilterDTO paginationFilterDto)        {


            //var queryEntities = GetEntities(session);

            var context = GetContext(session);
            var queryEntities = context.Contents.AsNoTracking().Include(x => x.ContentCategory).AsNoTracking();  
            
            if (paginationFilterDto.Title is not null && paginationFilterDto.Title != "null")
                queryEntities = queryEntities.Where(obj => obj.Title.Contains(paginationFilterDto.Title));

            if (paginationFilterDto.CountryId != 0)
                queryEntities = queryEntities.Where(obj => obj.CountryId == paginationFilterDto.CountryId);

            if (paginationFilterDto.CategoryId != 0)
                queryEntities = queryEntities.Where(obj => obj.ContentCategoryId == paginationFilterDto.CategoryId);

            if (paginationFilterDto.TypeId is not null)
                queryEntities = queryEntities.Where(obj => obj.ContentTypeId == paginationFilterDto.TypeId);            


            return await queryEntities.ToListAsync();

        }

        public async Task<Content> GetContentId(int id, ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.Contents.Where(u => u.Id == id).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Content>> GetContents(ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.Contents.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Content> UpdateContent(Content content, ContextSession session)
        {
            try
            {
                var context = GetContext(session);

                var objectExists = await Exists(content, session);

                context.Entry(content).State = objectExists ? EntityState.Modified : EntityState.Added;

                if (context.Entry(content).State == EntityState.Added)
                {
                    content.CreatedAt = DateTime.Now;
                    context.Contents.Add(content);
                }

                await context.SaveChangesAsync();
                return content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteContent(int id, ContextSession session)
        {
            var context = GetContext(session);
            var content = context.Contents.FirstOrDefault(x => x.Id.Equals(id));
            if (content == null) return false;
            else
                context.Contents.Remove(content);

            return await context.SaveChangesAsync() > 0 ? true : false;
        }








        /*public async Task<PagedResponseDTO<List<Content>>> GetAllNews(ContextSession session, ContentPaginationFilterDTO paginationFilterDto, bool includeDeleted = false)
        {
            var totalSkipped = (paginationFilterDto.PageNumber - 1) * paginationFilterDto.PerPage;

            var queryEntities = GetEntities(session);

            if (paginationFilterDto.Title is not null)
                queryEntities = queryEntities.Where(obj => obj.Title.Contains(paginationFilterDto.Title));

            if (paginationFilterDto.CountryId is not null)
                queryEntities = queryEntities.Where(obj => obj.CountryId == paginationFilterDto.CountryId);

            if (paginationFilterDto.CategoryId is not null)
                queryEntities = queryEntities.Where(obj => obj.ContentCategoryId == paginationFilterDto.CategoryId);

            /*if (paginationFilterDto.NewsTypeId is not null)
                queryEntities = queryEntities.Where(obj => obj.NewsTypeId == paginationFilterDto.NewsTypeId);#1#

            var newsList = await queryEntities
                .Skip(totalSkipped)
                .Take(paginationFilterDto.PerPage)
                .ToListAsync();

            var total = await queryEntities.CountAsync();
            var pageNumber = paginationFilterDto.PageNumber;
            var perPage = paginationFilterDto.PerPage;

            var pagedResponseDto = new PagedResponseDTO<List<Content>>(newsList, pageNumber, perPage, total);

            return pagedResponseDto;
        }*/
    }
}