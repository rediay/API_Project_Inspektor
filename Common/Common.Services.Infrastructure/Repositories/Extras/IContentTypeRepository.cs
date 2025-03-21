using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Extras
{
    public interface IContentTypeRepository
    {
        Task<PagedResponseDTO<List<ContentType>>> GetAll(ContextSession session, PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<ContentType> GetTypeContentId(int id, ContextSession session);
        Task<List<ContentType>> GetTypeContent(ContextSession session);
        Task<ContentType> UpdateTypeContent(ContentType contentType, ContextSession session);
        Task<bool> DeleteTypeContent(int id, ContextSession session);
    }


}