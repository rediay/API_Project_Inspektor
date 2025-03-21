using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Extras;
using Common.Services.Infrastructure.Services.Extras;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.Extras
{
    public class EventRoiOperationTypeRepository : BaseRepository<EventRoiOperationType, DataContext>, IEventRoiOperationTypeRepository<EventRoiOperationType>
    {
        public EventRoiOperationTypeRepository(DataContext context) : base(context)
        {
            
        }

        public Task<List<EventRoiOperationType>> GetAll(ContextSession session, bool includeDeleted = false)
        {
            var queryEntities =  GetEntities(session).ToListAsync();
            return queryEntities;
        }
        
        public async Task<EventRoiOperationType> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session).Where(obj => obj.Id == id).FirstOrDefaultAsync();
        }
    }
}