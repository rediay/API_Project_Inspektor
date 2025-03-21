using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Extras
{
    public interface IEventRoiRepository<TEventRoi> where TEventRoi : EventRoi
    {
        Task<TEventRoi> Edit(TEventRoi events, ContextSession session);
    }
}