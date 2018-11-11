using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ExpenseTracker.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public static class QueryableExtensions
    {
        public static async Task<TEntity> SingleElementAsync<TEntity>(this IQueryable<TEntity> queryable, Expression<Func<TEntity, bool>> predicate)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var result = await queryable.SingleOrDefaultAsync(predicate);

            if (result == null)
            {
                throw new NotFoundException(queryable.ElementType.Name);
            }

            return result;
        }
    }
}