using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Lists;
using Common.Entities.Relations_Countrys;

namespace Common.Services.Infrastructure.Services.Relations_Countrys
{
    public interface ICountryService
    {
        Task<PagedResponseDTO<List<Countries>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false);
        Task<IEnumerable<CountryDTO>> GetCountries();
        Task<ResponseDTO<CountryDTO>> Edit(CountryDTO dto);
        Task<ResponseDTO<bool>> BulkCreate(List<CountryDTO> dtos);
        Task<ResponseDTO<bool>> Delete(int id);
    }
}