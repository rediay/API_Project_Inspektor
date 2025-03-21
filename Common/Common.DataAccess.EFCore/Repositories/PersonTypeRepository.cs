using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class PersonTypeRepository : BaseRepository<PersonType, DataContext>, IPersonTypeRepository
    {
        public PersonTypeRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<PersonType>> GetAll(ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session).ToListAsync();
        }

        public async Task<PersonType> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}