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
    public class AccessSubModulesRepository :  IAccessSubModulesRepository
    {
        private readonly DataContext _dataContext;
        public AccessSubModulesRepository(DataContext context) 
        {
            _dataContext = context; 
           
        }
        protected DataContext GetContext(ContextSession session)
        {
            _dataContext.Session = session;
            return _dataContext;
        }


        public async Task<IEnumerable<AccessSubModule>> Update(IEnumerable<AccessSubModule> accessSubModules, ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                context.AccessSubModules.UpdateRange(accessSubModules);
                await context.SaveChangesAsync();

                return accessSubModules;
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

        public async Task<IEnumerable<AccessSubModule>> GetAccess(int id, ContextSession session)
        {
            var context = GetContext(session);
            return await context.AccessSubModules.Where(x => x.AccessId == id).ToListAsync();
        }

        public async Task<IEnumerable<AccessSubModule>> GetAllAccess(ContextSession session)
        {
            var context = GetContext(session);
            var user = context.Users.FirstOrDefault(x => x.Id == session.UserId);
            return await context.AccessSubModules.
                Include(x=>x.Access).Where(x=>x.Access.CompanyId==user.Id)
                .ToListAsync();
        }

       
    }
}
