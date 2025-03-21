using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Services.Extras
{
    public interface IContentService
    {
        Task<PagedResponseDTO<List<ContentDTO>>> GetAll(ContentPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        /*Task<PagedResponseDTO<List<ContentDTO>>> GetAllNews(ContentPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);*/
        Task<List<ContentExcelDTO>> GetAllToExcel(ContentPaginationFilterDTO paginationFilterDto);
        Task<ContentDTO> GetContentId(int id);
        Task<List<ContentDTO>> GetContents();
        Task<ContentDTO> UpdateContent(ContentDTO contentDTO);
        Task<bool> DeleteContent(int id);

    }
}