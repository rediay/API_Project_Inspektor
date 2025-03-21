using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Extras
{
    public interface IContentRepository<TNews> where TNews : Content
    {
        Task<PagedResponseDTO<List<TNews>>> GetAll(ContextSession session, ContentPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        /*Task<PagedResponseDTO<List<TNews>>> GetAllNews(ContextSession session, ContentPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);*/
        Task<List<TNews>> GetAllNews(ContextSession session, ContentPaginationFilterDTO paginationFilterDto);

        Task<Content> GetContentId(int id, ContextSession session);
        Task<List<Content>> GetContents(ContextSession session);
        Task<Content> UpdateContent(Content content, ContextSession session);
        Task<bool> DeleteContent(int id, ContextSession session);
    }
}