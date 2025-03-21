using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;

namespace Common.Services.Infrastructure.Services.RestrictiveLists
{
    public interface IListService
    {
        Task<PagedResponseDTO<List<ListDTO>>> GetAll(ListPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        
        Task<PagedResponseDTO<List<ListDTO>>> GetAllByValidation(ListPaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        
        Task<ResponseDTO<ListDTO>> GetById(int id, bool includeDeleted = false);
        Task<ResponseDTO<ListDTO>> Edit(ListDTO dto);

        Task<ResponseDTO<ListDTO>> Create(ListDTO dto);
        Task<ResponseDTO<bool>> BulkCreate(List<ListDTO> dtos);

        Task<bool> ValidateRecords(IEnumerable<int> listId);
    }
}