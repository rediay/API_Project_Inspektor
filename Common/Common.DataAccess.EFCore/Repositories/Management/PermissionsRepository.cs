/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Management;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.Management
{
    public class PermissionsRepository :  IPermissionsRepository
    {
        private readonly DataContext _dataContext;
        public PermissionsRepository(DataContext context) 
        {
            _dataContext = context; 
           
        }
        protected DataContext GetContext(ContextSession session)
        {
            _dataContext.Session = session;
            return _dataContext;
        }


        public async Task<IEnumerable<Permissions>> Update(IEnumerable<Permissions> permissions, ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                context.Permissions.UpdateRange(permissions);
                await context.SaveChangesAsync();

                return permissions;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Modules>> GetModules(ContextSession session)
        {
            var context = GetContext(session);
            return await  context.Modules.Include(x => x.SubModules).ToListAsync();

        }

        public async Task<IEnumerable<Permissions>> GetPermissionsByUser(int UserId, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Permissions.Where(x => x.UserId == UserId).ToListAsync();
        }
    }
}
