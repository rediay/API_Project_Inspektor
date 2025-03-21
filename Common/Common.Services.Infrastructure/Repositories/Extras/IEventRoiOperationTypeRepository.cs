using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Extras
{
    public interface IEventRoiOperationTypeRepository<TEventRoiTypeOperation>
        where TEventRoiTypeOperation : EventRoiOperationType
    {
        Task<List<TEventRoiTypeOperation>> GetAll(ContextSession session, bool includeDeleted = false);
        Task<TEventRoiTypeOperation> Get(int id, ContextSession session, bool includeDeleted = false);
    }
}