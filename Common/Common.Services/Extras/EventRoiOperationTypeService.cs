using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories.Extras;
using Common.Services.Infrastructure.Services.Extras;

namespace Common.Services.Extras
{
    public class EventRoiOperationTypeService : BaseService, IEventRoiOperationTypeService
    {
        private readonly IEventRoiOperationTypeRepository<EventRoiOperationType> _eventRoiOperationTypeRepository;

        public EventRoiOperationTypeService(ICurrentContextProvider contextProvider,
            IEventRoiOperationTypeRepository<EventRoiOperationType> eventRoiOperationTypeRepository) : base(
            contextProvider)
        {
            _eventRoiOperationTypeRepository = eventRoiOperationTypeRepository;
        }

        public Task<List<EventRoiOperationType>> GetAll(ContextSession session, bool includeDeleted = false)
        {
            return _eventRoiOperationTypeRepository.GetAll(Session);
        }
        
        public async Task<EventRoiOperationType> GetById(int id, bool includeDeleted = false)
        {
            return await _eventRoiOperationTypeRepository.Get(id, Session, includeDeleted);
        }
    }
}