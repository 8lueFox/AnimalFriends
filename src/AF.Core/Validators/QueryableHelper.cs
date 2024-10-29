using System.Linq.Expressions;
using LinqToDB;

namespace AF.Core.Validators;

internal class QueryableHelper
{
    internal static async ValueTask<bool> Any<TItem>(IQueryable<TItem> query, Expression<Func<TItem, bool>> predicate, CancellationToken cancellationToken)
    {
        if (query is IAsyncEnumerable<TItem>)
        {
            return await query.AnyAsync(predicate, cancellationToken);
        }

        return query.Any(predicate);
    }

    internal static async ValueTask<bool> All<TItem>(IQueryable<TItem> query, Expression<Func<TItem, bool>> predicate, CancellationToken cancellationToken)
    {
        if (query is IAsyncEnumerable<TItem>)
        {
            return await query.AllAsync(predicate, cancellationToken);
        }

        return query.All(predicate);
    }
}