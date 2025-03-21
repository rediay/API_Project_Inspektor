using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Common.DataAccess.EFCore.Repositories
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> GetAllIncluding<TEntity>(this IQueryable<TEntity> queryable, params string[] includeProperties) where TEntity : class
        {
            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public static IQueryable<TEntity> GetAllIncludingWithFunc<TEntity>(this IQueryable<TEntity> queryable, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class
        {
            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }        
    }
}