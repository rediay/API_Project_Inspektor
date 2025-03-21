using Common.Entities;
using Common.Services.Infrastructure.Management;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories.Management
{
    public class CompanyRepository : BaseRepository<Company, DataContext>, ICompanyRepository
    {
        public CompanyRepository(DataContext context) : base(context) { }

        public async Task<Company> GetCompany(ContextSession session)
        {

            try
            {
                var context = GetContext(session);
                var user = await context.Users.Where(u => u.Id == session.UserId).FirstOrDefaultAsync();
                var result = await context.Companies.Where(obj => obj.Id == user.CompanyId).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Company>> GetAllCompanies(ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.Companies.AsNoTracking().ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Company> UpdateCompany(Company company, ContextSession session)

        {
            try
            {
                var objectExists = await Exists(company, session);
                var context = GetContext(session);
                company.UpdatedAt = DateTime.Now;
                context.Entry(company).State = objectExists ? EntityState.Modified : EntityState.Added;

                if (context.Entry(company).State == EntityState.Added)
                {
                    company.CreatedAt = DateTime.Now;
                    context.Companies.Add(company);
                }

                await context.SaveChangesAsync();
                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> DeleteCompany(int id, ContextSession session)
        {
            var context = GetContext(session);
            var ownlist = context.Companies.FirstOrDefault(x => x.Id.Equals(id));
            if (ownlist == null) return false;
            else
            {
                ownlist.DeletedAt = DateTime.Now;
                ownlist.Status = false;
                //context.Companies.Remove(ownlist);
            }


            return await context.SaveChangesAsync() > 0 ? true : false;

        }

    }
}
