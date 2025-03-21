using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Extras;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.Extras
{
    public class EventRoiOperationStatusRepository : BaseRepository<EventRoiOperationStatus, DataContext>, IEventRoiOperationStatusRepository<EventRoiOperationStatus>
    {
        public EventRoiOperationStatusRepository(DataContext context) : base(context)
        {
        }

        public Task<List<EventRoiOperationStatus>> GetAll(ContextSession session, bool includeDeleted = false)
        {
            var queryEntities =  GetEntities(session).ToListAsync();
            return queryEntities;
        }

        public async Task<EventRoiOperationStatus> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session).Where(obj => obj.Id == id).FirstOrDefaultAsync();
        }
    }
}