﻿/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore
{
    public abstract class BaseRepository<TType, TContext>
        where TType : BaseEntity, new()
        where TContext : DataContext
    {
        private readonly TContext _dbContext;

        protected BaseRepository(TContext context)
        {
            _dbContext = context;
        }

        protected IQueryable<TType> GetEntities(ContextSession session)
        {
            var context = GetContext(session);
            return context.Set<TType>().AsQueryable().AsNoTracking();
        }

        protected TContext GetContext(ContextSession session)
        {
            _dbContext.Session = session;
            return _dbContext;
        }

        public virtual async Task<IEnumerable<TType>> GetList(ContextSession session)
        {
            return await GetEntities(session).ToListAsync();
        }

        public virtual async Task<TType> Get(int id, ContextSession session)
        {
            return await GetEntities(session)
                .Where(obj => obj.Id == id)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<bool> Exists(TType obj, ContextSession session)
        {
            return await GetEntities(session)
                       .Where(x => x.Id == obj.Id)
                       .CountAsync() > 0;
        }

        public virtual async Task<TType> Edit(TType obj, ContextSession session)
        {
            try
            {
                var objectExists = await Exists(obj, session);
                var context = GetContext(session);

                if (objectExists)
                {
                    obj.UpdatedAt = DateTime.Now;
                    context.Entry(obj).State = EntityState.Modified;
                }
                else
                {
                    obj.CreatedAt = DateTime.Now;
                    obj.UpdatedAt = DateTime.Now;
                    context.Entry(obj).State = EntityState.Added;
                }

                await context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task Delete(int id, ContextSession session)
        {
            var context = GetContext(session);
            var itemToDelete = new TType { Id = id };
            context.Entry(itemToDelete).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}