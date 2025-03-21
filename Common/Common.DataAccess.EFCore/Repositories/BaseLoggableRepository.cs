using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Common.DataAccess.EFCore.Repositories
{
    public abstract class BaseLoggableRepository<TType, TContext> : BaseSoftDeletableRepository<TType, TContext>
        where TType : BaseEntity, new()
        where TContext : DataContext
    {
        protected BaseLoggableRepository(TContext context) : base(context)
        {
        }

        public override async Task<TType> Edit(TType obj, ContextSession session)
        {
            var objectExists = await Exists(obj, session);
            var context = GetContext(session);

            if (objectExists)
            {
                var logRecord = await GenerateLogObject("UPDATED", obj, session);
                context.Entry(logRecord).State = EntityState.Added;


                var trackedEntity = context.Set<User>().Local.FirstOrDefault(u => u.Id == obj.Id);

                if (trackedEntity != null)
                {
                    context.Entry(trackedEntity).State = EntityState.Detached;
                }

                obj.UpdatedAt = DateTime.Now;
               
                context.Entry(obj).State = EntityState.Modified;
            }
            else
            {
                obj.CreatedAt = DateTime.Now;
                context.Entry(obj).State = EntityState.Added;
                await context.SaveChangesAsync();
                
                var logRecord = await GenerateLogObject("CREATED", obj, session);
                context.Entry(logRecord).State = EntityState.Added;
            }
            
            await context.SaveChangesAsync();
            return obj;
        }

        private async Task<Log> GenerateLogObject(string action, TType obj, ContextSession session)
        {
            var userId = session.UserId;
            var baseEntity = new TType();

            var currentRecordFullRelation = await GetContext(session).Set<TType>()
                .GetAllIncluding(obj.RelationshipNames)
                .Where(record => record.Id == obj.Id).AsNoTracking().FirstAsync();

            var json = JsonConvert.SerializeObject(currentRecordFullRelation);

            var logRecord = new Log
            {
                UserId = userId,
                Json = json,
                ModelId = obj.Id,
                ModelName = obj.GetType().Name,
                Action = action,
                CreatedAt = DateTime.Now
            };

            return logRecord;
        }
    }
}