using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Extras;
using Common.Services.Infrastructure.Services.Extras;

namespace Common.Services.Extras
{
    public class EventRoiOperationStatusService : BaseService, IEventRoiOperationStatusService
    {
        private readonly IEventRoiOperationStatusRepository<EventRoiOperationStatus> _eventRoiOperationStatusRepository;

        public EventRoiOperationStatusService(ICurrentContextProvider contextProvider,
            IEventRoiOperationStatusRepository<EventRoiOperationStatus> eventRoiOperationStatusRepository) : base(
            contextProvider)
        {
            _eventRoiOperationStatusRepository = eventRoiOperationStatusRepository;
        }

        public Task<List<EventRoiOperationStatus>> GetAll(ContextSession session, bool includeDeleted = false)
        {
            return _eventRoiOperationStatusRepository.GetAll(Session);
        }
        
        public async Task<EventRoiOperationStatus> GetById(int id, bool includeDeleted = false)
        {
            return await _eventRoiOperationStatusRepository.Get(id, Session, includeDeleted);
        }
    }
}