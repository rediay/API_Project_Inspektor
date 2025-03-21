using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories.Extras
{
    public interface IContentStatusesRepository
    {
        #region CRUD STATE
        Task<ContentStatus> GetStateId(int id, ContextSession session);
        Task<List<ContentStatus>> GetStates(ContextSession session);
        Task<ContentStatus> UpdateState(ContentStatus contentStatus, ContextSession session);
        Task<bool> DeleteState(int id, ContextSession session);
        #endregion
    }
}
