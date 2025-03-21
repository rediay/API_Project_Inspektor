using Common.Entities;
using Common.Services.Infrastructure.Repositories.Extras;

namespace Common.DataAccess.EFCore.Repositories.Extras
{
    public class EventRoiRepository : BaseRepository<EventRoi, DataContext>, IEventRoiRepository<EventRoi>
    {
        public EventRoiRepository(DataContext context) : base(context)
        {
        }
    }
}