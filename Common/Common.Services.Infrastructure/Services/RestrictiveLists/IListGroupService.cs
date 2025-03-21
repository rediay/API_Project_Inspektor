using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.RestrictiveLists;
using Common.DTO.Users;

namespace Common.Services.Infrastructure.Services.RestrictiveLists
{
    public interface IListGroupService
    {
        Task<PagedResponseDTO<List<ListGroupDTO>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);

        Task<ResponseDTO<ListGroupDTO>> GetById(int id, bool includeDeleted = false);

        Task<ResponseDTO<ListGroupDTO>> Edit(ListGroupDTO dto);

        Task<ResponseDTO<ListGroupDTO>> Create(ListGroupDTO dto);

        ResponseDTO<bool> Delete(int id);
    }
}