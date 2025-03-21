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
    public class PlanRepository : BaseLoggableRepository<Plan, DataContext>, IPlanRepository
    {
        public PlanRepository(DataContext context) : base(context) { }
        
        public Task<Plan> Get(int id, ContextSession session)
        {
            return null;
        }

        public async Task<List<Plan>> GetPlans(ContextSession session)
        {
            try
            {
                var context = GetContext(session);
                var result = await context.Plans.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Plan> UpdatePlan(Plan plan, ContextSession session)

        {
            try
            {
                var objectExists = await Exists(plan, session);
                var context = GetContext(session);
                plan.UpdatedAt = DateTime.Now;
                plan.UserId = session.UserId;
                context.Entry(plan).State = objectExists ? EntityState.Modified : EntityState.Added;

                if (context.Entry(plan).State == EntityState.Added)
                {
                    plan.CreatedAt = DateTime.Now;
                    context.Plans.Add(plan);
                }

                await context.SaveChangesAsync();
                return plan;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

  
    }
}
