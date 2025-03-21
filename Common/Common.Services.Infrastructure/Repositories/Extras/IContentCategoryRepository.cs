using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Extras
{
    public interface IContentCategoryRepository
    {
        Task<PagedResponseDTO<List<ContentCategory>>> GetAll(ContextSession session,
            PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        #region CRUD CATEGORY
        Task<ContentCategory> GetCategoryId(int id, ContextSession session);
        Task<List<ContentCategory>> GetCategorys(ContextSession session);
        Task<List<ContentCategory>> GetCategorysbyType(int idtype,ContextSession session);
        Task<ContentCategory> UpdateCategory(ContentCategory contentCategory, ContextSession session);
        Task<bool> DeleteCategory(int id, ContextSession session);
        #endregion
    }
}