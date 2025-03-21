/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTO.Management;
using Common.Entities;
using Common.Services.Infrastructure.Repositories.Management;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories.Management
{
    public class ThirdPartyTypeRepository : BaseRepository<ThirdPartyType, DataContext>, IThirdPartyTypeRepository
    {
        private readonly IHttpContextAccessor _accessor;
        public ThirdPartyTypeRepository(DataContext context, IHttpContextAccessor accessor) : base(context)
        {
            _accessor = accessor;
        }



        public async Task<List<ThirdPartyType>> GetByCompanyId(ContextSession session)
        {
            try
            {

                var claim = _accessor.HttpContext.User.FindFirst("CompanyID");
                if (claim != null && !string.IsNullOrEmpty(claim.Value))
                {
                    var context = GetContext(session);
                    var userCompanyId = Convert.ToInt32(claim.Value);
                    //var user = context.Users.Where(u => u.Id == session.UserId).FirstOrDefault();
                    //Console.WriteLine(user.CompanyId);
                    var result = await context.ThirdPartyTypeList
                       .Include(c => c.Company)
                        .Include(us => us.User)
                       .Where(obj => obj.CompanyId == userCompanyId)
                        //.Where(obj => obj.CompanyId == user.CompanyId)
                        .AsNoTracking().ToListAsync();
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public async Task<ThirdPartyType> UpdateThirdPartyType(ThirdPartyType thirdPartyType, ContextSession session)
        {


            var objectExists = await Exists(thirdPartyType, session);
            var context = GetContext(session);
            thirdPartyType.UpdatedAt = DateTime.Now;
            context.Entry(thirdPartyType).State = objectExists ? EntityState.Modified : EntityState.Added;

            if (context.Entry(thirdPartyType).State == EntityState.Added)
            {
                var user = await context.Users.Where(u => u.Id == session.UserId).FirstOrDefaultAsync();
                thirdPartyType.UserId = user.Id;
                thirdPartyType.CompanyId = user.CompanyId;
                thirdPartyType.CreatedAt = DateTime.Now;
                context.ThirdPartyTypeList.Add(thirdPartyType);
            }

            await context.SaveChangesAsync();
            return thirdPartyType;

        }
        public async Task<bool> DeleteThirdPartyType(int id, ContextSession session)
        {
            var context = GetContext(session);
            var claim = _accessor.HttpContext.User.FindFirst("CompanyID");
            if (claim != null && !string.IsNullOrEmpty(claim.Value))
            {
                var userCompanyId = Convert.ToInt32(claim.Value);
                var ownlist = context.ThirdPartyTypeList.Where(x => x.CompanyId == userCompanyId).FirstOrDefault(x => x.Id.Equals(id));
                if (ownlist == null) return false;
                else
                    context.ThirdPartyTypeList.Remove(ownlist);

                return await context.SaveChangesAsync() > 0 ? true : false;
            }
            return false;

        }
    }
}
