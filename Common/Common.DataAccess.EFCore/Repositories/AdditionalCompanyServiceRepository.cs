using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class AdditionalCompanyServiceRepository : BaseLoggableRepository<AdditionalCompanyService, DataContext>,
        IAdditionalCompanyServiceRepository
    {
        public AdditionalCompanyServiceRepository(DataContext context) : base(context)
        {
        }

        public Task<List<AdditionalCompanyService>> GetAll(ContextSession session, int companyId)
        {
            var queryEntities = GetEntities(session)
                .Where(obj => obj.CompanyId == companyId)
                .Include(obj => obj.AdditionalService)
                .Include(obj => obj.Company);

            return queryEntities.ToListAsync();
        }

        /*public Task<AdditionalCompanyService> Edit(ContextSession session, AdditionalCompanyService record)
        {
            return Edit(session, record);
        }*/
    }
}