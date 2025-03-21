using Common.DTO;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Repositories.Management
{
    public interface IPermissionsRepository
    {
        Task<IEnumerable<Permissions>> Update(IEnumerable<Permissions> permissions, ContextSession session);
        Task<IEnumerable<Modules>> GetModules( ContextSession session);
        Task<IEnumerable<Permissions>> GetPermissionsByUser(int UserId,ContextSession session);
    }
}
