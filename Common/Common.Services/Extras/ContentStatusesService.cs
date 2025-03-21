using Common.Services.Infrastructure.Repositories.Extras;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Services.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Utils;

namespace Common.Services.Extras
{
    public class ContentStatusesService : BaseService, IContentStatusesServices
    {
        private readonly IContentStatusesRepository _contentStatusesRepository;

        public ContentStatusesService(ICurrentContextProvider contextProvider,
            IContentStatusesRepository contentStatusesRepository) : base(contextProvider)
        {
            _contentStatusesRepository = contentStatusesRepository;
        }

        #region CRUD STATE


        public async Task<ContentStatusDTO> GetStateId(int id)
        {
            var state = await _contentStatusesRepository.GetStateId(id, Session);
            var map = state.MapTo<ContentStatusDTO>();
            return map;
        }

        public async Task<List<ContentStatusDTO>> GetStates()
        {
            try
            {
                var states = await _contentStatusesRepository.GetStates(Session);
                var map = states.MapTo<List<ContentStatusDTO>>();
                return map;
            }
            catch (Exception ex)
            {
                return await new Task<List<ContentStatusDTO>>(null);
            }

        }

        public async Task<ContentStatusDTO> UpdateState(ContentStatusDTO stateDTO)
        {
            try
            {
                var state = stateDTO.MapTo<ContentStatus>();
                var stateDTOs = await _contentStatusesRepository.UpdateState(state, Session);
                return stateDTOs.MapTo<ContentStatusDTO>();
            }
            catch (Exception ex)
            {
                return await new Task<ContentStatusDTO>(null);
            }
        }

        public async Task<bool> DeleteState(int id)
        {
            await _contentStatusesRepository.DeleteState(id, Session);
            return true;
        }

        #endregion
    }
}
