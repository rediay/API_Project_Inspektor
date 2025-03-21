using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services.Extras
{
    public interface IContentTypeService
    {
        Task<PagedResponseDTO<List<ContentType>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        #region CRUD TypeContent
        Task<ContentTypeDTO> GetTypeContentId(int id);
        Task<List<ContentTypeDTO>> GetTypeContent();
        Task<ContentTypeDTO> UpdateTypeContent(ContentTypeDTO typecontentDTO);
        Task<bool> DeleteTypeContent(int id);
        #endregion
    }


}