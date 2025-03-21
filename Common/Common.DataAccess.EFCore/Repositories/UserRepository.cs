/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore
{
    public class UserRepository : BaseDeletableRepository<User, DataContext>, IUserRepository<User>
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public override async Task<User> Edit(User obj, ContextSession session)
        {
                var objectExists = await Exists(obj, session);
                var state = objectExists ? EntityState.Modified : EntityState.Added;
                var context = GetContext(session);
                context.Entry(obj).State = state;

                if (string.IsNullOrEmpty(obj.Password))
                {
                    context.Entry(obj).Property(x => x.Password).IsModified = false;
                }

                await context.SaveChangesAsync();
                return obj;
            
        }

        public override async Task<User> Get(int id, ContextSession session, bool includeDeleted = false)
        {
            try
            {
                var x = await _dataContext.Users.Where(obj => obj.Id == id)
                    .Include(u => u.UserRoles)
                    .ThenInclude(x => x.Role)
                    .Include(u => u.Settings)
                    .Include(u => u.Permissions)
                    .FirstOrDefaultAsync();                
                return x;
                //return await GetEntities(session, includeDeleted)
                //    .Where(obj => obj.Id == id)
                //    .Include(u => u.UserRoles)
                //    .ThenInclude(x => x.Role)
                //    .Include(u => u.Settings)                    
                //    .Include(u => u.Permissions)
                //    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<User> GetByLogin(string login, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session, includeDeleted)
                .Where(obj => obj.Login == login)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .Include(u => u.Settings)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmail(string email, ContextSession session, bool includeDeleted = false)
        {
            return await GetEntities(session, includeDeleted)
                .Where(obj => obj.Email == email)
                .Include(u => u.UserRoles)
                .ThenInclude(x => x.Role)
                .Include(u => u.Settings)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> ValidateCompanyIsActive(int userId, ContextSession session)
        {
            try
            {
                var user = await _dataContext.Users.Where(obj => obj.Id == userId)
                    .Include(u => u.Company)
                    .FirstOrDefaultAsync();
                return user.Company.Status;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}