using Common.Entities;
using Common.Services.Infrastructure.Repositories.OwnLists;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories.OwnLists
{
    public class OwnListsTypeRepository : BaseRepository<OwnListType, DataContext>, IOwnListsTypesRepository
    {
        private readonly IHttpContextAccessor _accessor;
        public OwnListsTypeRepository(DataContext context, IHttpContextAccessor accessor) : base(context)
        {
            _accessor = accessor;
        }

        public async Task<List<OwnListType>> GetOwnListTypes(int companyId, ContextSession session)
        {
            var context = GetContext(session);
            var user = context.Users.Where(u => u.Id == session.UserId).FirstOrDefault();
            var result = await context.OwnListTypes.Where(obj => obj.CompanyId == user.CompanyId).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<bool> UpdateOwnListType(OwnListType ownListType, ContextSession session)
        {

            var objectExists = await Exists(ownListType, session);
            var context = GetContext(session);
            var user = await context.Users.Where(u => u.Id == session.UserId).FirstOrDefaultAsync();
            ownListType.UpdatedAt = DateTime.Now;
            context.Entry(ownListType).State = objectExists ? EntityState.Modified : EntityState.Added;

            if (context.Entry(ownListType).State == EntityState.Added)
            {
                ownListType.CreatedAt = DateTime.Now;
                ownListType.CompanyId = user.CompanyId;
                ownListType.UserId = user.Id;

                context.OwnListTypes.Add(ownListType);
            }


            return await context.SaveChangesAsync() > 0 ? true : false;

        }
        public async Task<bool> CreateOwnListType(OwnListType ownListType, ContextSession session)
        {

            //var context = GetContext(session);
            //var objectExists = await Exists(company, session);
            //var context = GetContext(session);
            //company.UpdatedAt = DateTime.Now;
            //context.Entry(company).State = objectExists ? EntityState.Modified : EntityState.Added;

            //if (context.Entry(company).State == EntityState.Added)
            //{
            //    company.CreatedAt = DateTime.Now;
            //    context.Companies.Add(company);
            //}


            return false;

        }
        public async Task<bool> DeleteOwnListType(int id, ContextSession session)
        {
            var context = GetContext(session);
            var ownlist = context.OwnListTypes.FirstOrDefault(x => x.Id.Equals(id));
            if (ownlist == null) return false;
            else
                context.OwnListTypes.Remove(ownlist);

            return await context.SaveChangesAsync() > 0 ? true : false;

        }

        public async Task<bool> ImportOwnLists(List<OwnList> ownLists, ContextSession session)
        {
            var context = GetContext(session);
            await context.AddRangeAsync(ownLists);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteImportedOwnListsByType(int ownListTypeId, ContextSession session)
        {
            //_accessor.HttpContext.User
            var claim = _accessor.HttpContext.User.FindFirst("CompanyID");
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                var userCompanyId = Convert.ToInt32(claim.Value);
                var context = GetContext(session);
                var ownListType = context.OwnListTypes.Where(o => o.CompanyId == userCompanyId && o.Id == ownListTypeId).First();

                if (ownListType == null) return false;

                var lists = context.OwnLists.Where(obj => obj.OwnListTypeId == ownListTypeId);
                context.RemoveRange(lists);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
