using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories.Management
{
    public interface IThirdPartyTypeRepository
    {
        Task<List<ThirdPartyType>> GetByCompanyId( ContextSession session);        
        Task<ThirdPartyType> UpdateThirdPartyType(ThirdPartyType thirdPartyType, ContextSession session);
        Task<bool> DeleteThirdPartyType(int id, ContextSession session);
    }
}
