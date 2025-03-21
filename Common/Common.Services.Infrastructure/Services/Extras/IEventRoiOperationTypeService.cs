using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Services.Extras
{
    public interface IEventRoiOperationTypeService
    {
        Task<List<EventRoiOperationType>> GetAll(ContextSession session, bool includeDeleted = false);
        Task<EventRoiOperationType> GetById(int id, bool includeDeleted = false);
    }
}