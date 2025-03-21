using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Relations_Countrys;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.Services.Infrastructure.Services.Relations_Countrys
{
    public interface IStatesService
    {
        Task<PagedResponseDTO<List<States>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<IEnumerable<StatesDTO>> GetStatesbyId(int idcountry);
        Task<ResponseDTO<StatesDTO>> Edit(StatesDTO dto);
        Task<ResponseDTO<bool>> BulkCreate(List<StatesDTO> dtos);
        Task<ResponseDTO<bool>> Delete(int id);
    }
}