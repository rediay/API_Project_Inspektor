using Common.DTO;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories.Management
{
    public interface IAccessSubModulesRepository
    {
        Task<IEnumerable<AccessSubModule>> Update(IEnumerable<AccessSubModule> accessSubModules, ContextSession session);
        Task<IEnumerable<Modules>> GetModules( ContextSession session);
        Task<IEnumerable<AccessSubModule>> GetAccess(int id,ContextSession session);
        
    }
}
