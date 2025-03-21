using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IPersonTypeRepository
    {
        Task<List<PersonType>> GetAll(ContextSession session,
            bool includeDeleted = false);
        Task<PersonType> Get(int id, ContextSession session, bool includeDeleted = false);
    }
}