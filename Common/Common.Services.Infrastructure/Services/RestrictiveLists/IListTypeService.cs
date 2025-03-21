using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using ListTypeDTO = Common.DTO.RestrictiveLists.ListTypeDTO;

namespace Common.Services.Infrastructure.Services.RestrictiveLists
{
    public interface IListTypeService
    {
        Task<ResponseDTO<List<ListTypeDTO>>> GetAll(bool includeDeleted = false);

        Task<PagedResponseDTO<List<ListTypeDTO>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<ResponseDTO<ListTypeDTO>> GetById(int id, bool includeDeleted = false);

        Task<ResponseDTO<ListTypeDTO>> Edit(ListTypeDTO dto);

        Task<ResponseDTO<ListTypeDTO>> Create(ListTypeDTO dto);
        
        ResponseDTO<bool> Delete(int id);
    }
}