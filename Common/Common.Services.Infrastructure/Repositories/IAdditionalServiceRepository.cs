using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IAdditionalServiceRepository
    {
        Task<List<AdditionalService>> GetAll(ContextSession session);
    }
}