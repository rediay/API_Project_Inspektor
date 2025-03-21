using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories.Extras
{
    public interface IEventRepository<TEvents> where TEvents : EventRoi
    {
        Task<TEvents> Edit(TEvents events, ContextSession session);
    }
}