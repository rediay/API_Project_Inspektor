using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface IAdditionalCompanyServiceRepository
    {
        Task<List<AdditionalCompanyService>> GetAll(ContextSession session, int companyId);
        //Task<AdditionalCompanyService> Edit(ContextSession session, AdditionalCompanyService record);
        Task<AdditionalCompanyService> Edit(AdditionalCompanyService record, ContextSession session);
    }
}