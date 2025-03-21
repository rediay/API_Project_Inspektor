using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Services.Extras
{
    public interface IEventRoiOperationStatusService
    {
        Task<List<EventRoiOperationStatus>> GetAll(ContextSession session, bool includeDeleted = false);
        Task<EventRoiOperationStatus> GetById(int id, bool includeDeleted = false);
    }
}