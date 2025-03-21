using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.DTO.Lists;
using Common.Entities.Relations_Countrys;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Repositories.Relations_Countrys;
using Common.Services.Infrastructure.Services.Lists;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Common.Utils;

namespace Common.Services
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICurrentContextProvider contextProvider, ICountryRepository countryRepository) : base(
            contextProvider)
        {
            _countryRepository = countryRepository;
        }

        public async Task<PagedResponseDTO<List<Countries>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _countryRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            return pagedResponse;
        }

        public async Task<IEnumerable<CountryDTO>> GetCountries()
        {
            var countries = await _countryRepository.GetCountries(Session);            
            return countries.MapTo<IEnumerable<CountryDTO>>(); ;
        }

        public async Task<ResponseDTO<CountryDTO>> Edit(CountryDTO dto)
        {
            var record = dto.MapTo<Countries>();
            var newRecord = await _countryRepository.Edit(record, Session);
            var recordMapped = newRecord.MapTo<CountryDTO>();
            var response = new ResponseDTO<CountryDTO>(recordMapped);
            return response;
        }

        public async Task<ResponseDTO<bool>> BulkCreate(List<CountryDTO> dtos)
        {
            var records = dtos.Select(list => list.MapTo<Countries>()).ToList();
            var status = await _countryRepository.BulkCreate(records, Session);
            var response = new ResponseDTO<bool>(status);
            return response;
        }

        public async Task<ResponseDTO<bool>> Delete(int id)
        {
            var status = await _countryRepository.DeleteItem(id, Session);
            var response = new ResponseDTO<bool>( status);
            return response;
        }
    }
}