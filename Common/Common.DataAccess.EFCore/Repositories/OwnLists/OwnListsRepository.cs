using Common.Entities;
using Common.Services.Infrastructure.Repositories.OwnLists;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories.OwnLists
{
    public class OwnListsRepository : BaseRepository<OwnList, DataContext>, IOwnListsRepository
    {
        public OwnListsRepository(DataContext context) : base(context) { }

        public async Task<List<OwnList>> GetOwnLists(int companyId, ContextSession session)
        {
            var context = GetContext(session);
            var result = await context.OwnLists.Where(obj => obj.Company.Id == companyId).Include(x=>x.OwnListType).Include(x=>x.Company).Include(x=>x.User).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<bool> UpdateOwnList(OwnList ownList, ContextSession session)
        {
            var objectExists = await Exists(ownList, session);
            var context = GetContext(session);
            var user = await context.Users.Where(u => u.Id == session.UserId).FirstOrDefaultAsync();
            ownList.UpdatedAt = DateTime.Now;
            context.Entry(ownList).State = objectExists ? EntityState.Modified : EntityState.Added;

            if (context.Entry(ownList).State == EntityState.Added)
            {
                ownList.CreatedAt = DateTime.Now;
                ownList. CompanyId= user.CompanyId;
                ownList.UserId = user.Id;
                context.OwnLists.Add(ownList);
            }


            return await context.SaveChangesAsync() > 0 ? true : false;

        }
        public async Task<bool> CreateOwnList(OwnList ownList, ContextSession session)
        {
            var context = GetContext(session);
            context.OwnLists.Add(ownList);

            return await context.SaveChangesAsync() > 0 ? true : false;

        }
        public async Task<bool> DeleteOwnList(int id, ContextSession session)
        {
            var context = GetContext(session);
            var ownlist =context.OwnLists.FirstOrDefault(x => x.Id.Equals(id));           
            if (ownlist == null) return false;
            else
                context.OwnLists.Remove(ownlist);

            return await context.SaveChangesAsync() > 0 ? true : false;

        }

    }
}
