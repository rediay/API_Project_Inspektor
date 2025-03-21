using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services.Extras
{
    public interface IContentCategoryService
    {
        Task<PagedResponseDTO<List<ContentCategory>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        #region CRUD CATEGORY
        Task<ContentCategoryDTO> GetCategoryId(int id);
        Task<List<ContentCategoryDTO>> GetCategorys();
        Task<List<ContentCategoryDTO>> GetCategorysbyType(int idtype);
        Task<ContentCategoryDTO> UpdateCategory(ContentCategoryDTO categoyDTO);
        Task<bool> DeleteCategory(int id);
        #endregion
    }
}