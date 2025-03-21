/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.Entities;
using Common.Services.Infrastructure.Management;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore
{
    public class AccessRepository : BaseRepository<Access, DataContext>, IAccessRepository<Access>
    {
        public AccessRepository(DataContext context) : base(context) { }

        public override async Task<bool> Exists(Access obj, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Access.Where(x => x.Id == obj.Id).AsNoTracking().CountAsync() > 0;
        }

        public async Task<ICollection<Access>> Get(ContextSession session)
        {
            var context = GetContext(session);
            return await context.Access.AsNoTracking().Include(x => x.Company)
                .Include(x => x.AccessSubModules)
                .ThenInclude(o => o.SubModule)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Access> GetById(int id, ContextSession session)
        {
            var context = GetContext(session);
            return await context.Access.Where(obj => obj.Id == id)
                                        .Include(x => x.Company)
                                        .AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<ICollection<Access>> GetAccesByCompany(ContextSession session)
        {
            var context = GetContext(session);
            var user = context.Users.FirstOrDefault(x => x.Id.Equals(session.UserId));
            return await context.Access.Where(obj => obj.CompanyId == user.CompanyId)
                                         .Include(x => x.Company)
            .AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Access>> GetAccesByIdCompany(ContextSession session, int IdCompany)
        {
            var context = GetContext(session);
            return await context.Access.Where(obj => obj.CompanyId == IdCompany)
                                         .Include(x => x.Company)
                                        .AsNoTracking().ToListAsync();
        }

        public override Task<Access> Edit(Access obj, ContextSession session)
        {
            var context = GetContext(session);
            obj.CompanyId = obj.CompanyId > 0 ? obj.CompanyId : context.Users.FirstOrDefault(x => x.Id.Equals(session.UserId)).CompanyId;
            return base.Edit(obj, session);
        }


    }
}
