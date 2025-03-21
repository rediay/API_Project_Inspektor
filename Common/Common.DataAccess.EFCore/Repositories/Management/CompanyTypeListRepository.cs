using Common.Entities;
using Common.Services.Infrastructure.Management;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories.Management
{
    public class CompanyTypeListRepository : BaseRepository<CompanyTypeList, DataContext>, ICompanyTypeListRepository
    {
        public CompanyTypeListRepository(DataContext context) : base(context) { }

        public async Task<List<CompanyTypeList>> GetTypeList(ContextSession session)
        {
            var context = GetContext(session);
            var user = await context.Users.Where(u => u.Id == session.UserId).FirstOrDefaultAsync();
            var result = await context.CompanyTypeList.Where(obj => obj.Company.Id == user.CompanyId)
                .Include(x => x.ListType)
                .Include(x => x.User)
                .Include(x => x.ListType)
                .ThenInclude(ListType => ListType.ListGroup)
                .Include(l => l.ListType)
                .ThenInclude(c => c.Country)
                .Include(l => l.ListType)
                .ThenInclude(c => c.Periodicity)
                .AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<bool> UpdateRangeTypeList(bool status, ContextSession session)
        {
            var context = GetContext(session);
            var user = await context.Users.Where(u => u.Id == session.UserId).FirstOrDefaultAsync();
            var lstypelist = context.CompanyTypeList.Where(obj => obj.Company.Id == user.CompanyId && obj.Search != status).ToList();
            lstypelist.ForEach(obj => obj.Search = status);
            lstypelist.ForEach(obj => obj.UpdatedAt = DateTime.Now);
            context.CompanyTypeList.UpdateRange(lstypelist);
            return await context.SaveChangesAsync() > 0 ? true : false;

        }
        public async Task<bool> UpdateTypeList(CompanyTypeList lsCompanyTypeList, ContextSession session)
        {
            var context = GetContext(session);
            lsCompanyTypeList.UpdatedAt = DateTime.Now;
            context.CompanyTypeList.Update(lsCompanyTypeList);
            return await context.SaveChangesAsync() > 0 ? true : false;

        }
    }
}
