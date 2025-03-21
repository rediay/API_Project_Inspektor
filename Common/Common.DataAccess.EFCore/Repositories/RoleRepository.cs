/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore
{
    public class RoleRepository: BaseRepository<Role, DataContext>, IRoleRepository<Role>
    {
        public RoleRepository(DataContext context) : base(context)
        {
        }

        public async Task<Role> Get(string name, ContextSession session)
        {
            return await GetEntities(session)
                .Where(obj => obj.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}