using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DataAccess.EFCore.Repositories;
using Common.DTO;
using Common.DTO.Relations_Countrys;
using Common.Entities;
using Common.Entities.Relations_Countrys;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Relations_Countrys;
using Common.Services.Infrastructure.Services.Relations_Countrys;
using Common.Utils;

namespace Common.Services
{
    public class CitiesService : BaseService, ICitiesService
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesService(ICurrentContextProvider contextProvider, ICitiesRepository citiesRepository) : base(
            contextProvider)
        {
            _citiesRepository = citiesRepository;
        }

        public async Task<PagedResponseDTO<List<Cities>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _citiesRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            return pagedResponse;
        }

        public async Task<IEnumerable<CitiesDTO>> GetCitiesbyId(int idcountry, int stateid)
        {
            var states = await _citiesRepository.GetCitiesbyId(idcountry,stateid,Session);            
            return states.MapTo<IEnumerable<CitiesDTO>>(); ;
        }

        public async Task<ResponseDTO<CitiesDTO>> Edit(CitiesDTO dto)
        {
            var record = dto.MapTo<Cities>();
            var newRecord = await _citiesRepository.Edit(record, Session);
            var recordMapped = newRecord.MapTo<CitiesDTO>();
            var response = new ResponseDTO<CitiesDTO>(recordMapped);
            return response;
        }

        public async Task<ResponseDTO<bool>> BulkCreate(List<CitiesDTO> dtos)
        {
            var records = dtos.Select(list => list.MapTo<Cities>()).ToList();
            var status = await _citiesRepository.BulkCreate(records, Session);
            var response = new ResponseDTO<bool>(status);
            return response;
        }

        public async Task<ResponseDTO<bool>> Delete(int id)
        {
            var status = await _citiesRepository.Delete(id, Session);
            var response = new ResponseDTO<bool>(status);
            return response;
        }
    }
}