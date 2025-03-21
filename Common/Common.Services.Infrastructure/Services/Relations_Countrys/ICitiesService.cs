using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Relations_Countrys;
using Common.DTO.RestrictiveLists;
using Common.Entities;
using Common.Entities.Relations_Countrys;

namespace Common.Services.Infrastructure.Services.Relations_Countrys
{
    public interface ICitiesService
    {
        Task<PagedResponseDTO<List<Cities>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<IEnumerable<CitiesDTO>> GetCitiesbyId(int idcountry,int stateid);
        Task<ResponseDTO<CitiesDTO>> Edit(CitiesDTO dto);
        Task<ResponseDTO<bool>> BulkCreate(List<CitiesDTO> dtos);
        Task<ResponseDTO<bool>> Delete(int id);
    }
}