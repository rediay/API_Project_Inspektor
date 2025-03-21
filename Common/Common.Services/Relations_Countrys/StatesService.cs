using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class StatesService : BaseService, IStatesService
    {
        private readonly IStatesRepository _statesRepository;

        public StatesService(ICurrentContextProvider contextProvider, IStatesRepository statesRepository) : base(
            contextProvider)
        {
            _statesRepository = statesRepository;
        }

        public async Task<PagedResponseDTO<List<States>>> GetAll(PaginationFilterDTO paginationFilterDto,
            bool includeDeleted = false)
        {
            var pagedResponse = await _statesRepository.GetAll(Session, paginationFilterDto, includeDeleted);
            return pagedResponse;
        }

        public async Task<IEnumerable<StatesDTO>> GetStatesbyId(int idcountry)
        {
            var states = await _statesRepository.GetStatesById(idcountry,Session);            
            return states.MapTo<IEnumerable<StatesDTO>>(); ;
        }

        public async Task<ResponseDTO<StatesDTO>> Edit(StatesDTO dto)
        {
            var record = dto.MapTo<States>();
            var newRecord = await _statesRepository.Edit(record, Session);
            var recordMapped = newRecord.MapTo<StatesDTO>();
            var response = new ResponseDTO<StatesDTO>(recordMapped);
            return response;
        }

        public async Task<ResponseDTO<bool>> BulkCreate(List<StatesDTO> dtos)
        {
            var records = dtos.Select(list => list.MapTo<States>()).ToList();
            var status = await _statesRepository.BulkCreate(records, Session);
            var response = new ResponseDTO<bool>(status);
            return response;
        }

        public async Task<ResponseDTO<bool>> Delete(int id)
        {
            var status = await _statesRepository.Delete(id, Session);
            var response = new ResponseDTO<bool>(status);
            return response;
        }
    }
}