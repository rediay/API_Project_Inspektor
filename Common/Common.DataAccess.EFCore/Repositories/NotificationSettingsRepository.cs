/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class NotificationSettingsRepository : BaseRepository<NotificationSettings, DataContext>, INotificationSettingsRepository
    {
        public NotificationSettingsRepository(DataContext context) : base(context) { }

        public async Task<NotificationSettings> GetByCompanyId(int companyId, ContextSession session)
        {
            var context = GetContext(session);
            return await context.NotificationSettings.Where(obj => obj.CompanyId == companyId).AsNoTracking().FirstOrDefaultAsync();
        }



        //public override async Task<NotificationSettings> GetByCompanyId(int companyId, ContextSession session)
        //{
        //    var context = GetContext(session);
        //    return await context.NotificationSettings.Where(obj => obj.CompanyId == companyId).AsNoTracking().FirstOrDefaultAsync();
        //}

        async Task<NotificationSettings> INotificationSettingsRepository.Edit(NotificationSettings obj, ContextSession session)
        {

            var objectExists = await Exists(obj, session);
            var context = GetContext(session);
            if (!objectExists)
                obj.CreatedAt = DateTime.Now;
            obj.UpdatedAt = DateTime.Now;
            context.Entry(obj).State = objectExists ? EntityState.Modified : EntityState.Added;
            await context.SaveChangesAsync();
            return obj;
        }
    }
}
