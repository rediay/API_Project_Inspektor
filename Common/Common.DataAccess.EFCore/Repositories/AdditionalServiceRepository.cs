using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class AdditionalServiceRepository: BaseRepository<AdditionalService, DataContext>, IAdditionalServiceRepository
    {
        public AdditionalServiceRepository(DataContext context) : base(context)
        {
        }
        
        public async Task<List<AdditionalService>> GetAll(ContextSession session)
        {
            var records = await GetList(session);
            return records.ToList();
        }
    }
}