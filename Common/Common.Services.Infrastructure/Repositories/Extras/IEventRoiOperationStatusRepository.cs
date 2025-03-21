using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Extras
{
    public interface IEventRoiOperationStatusRepository<TEventRoiStatusOperation> where TEventRoiStatusOperation : EventRoiOperationStatus
    {
        Task<List<TEventRoiStatusOperation>> GetAll(ContextSession session, bool includeDeleted = false);
        Task<TEventRoiStatusOperation> Get(int id, ContextSession session, bool includeDeleted = false);
    }
}