using Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Services.Extras
{
    public interface IContentStatusesServices
    {
        #region CRUD STATE
        Task<ContentStatusDTO> GetStateId(int id);
        Task<List<ContentStatusDTO>> GetStates();
        Task<ContentStatusDTO> UpdateState(ContentStatusDTO stateDTO);
        Task<bool> DeleteState(int id);
        #endregion
    }
}
