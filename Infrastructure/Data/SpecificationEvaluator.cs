using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec == null) return query;

        // Filtering
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        // Sorting
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDesc != null)
        {
            query = query.OrderByDescending(spec.OrderByDesc);
        }

        // Distinct (entity level)
        if (spec.IsDistinct)
        {
            query = query.Distinct();
        }

        return query;
    }

    public static IQueryable<TResult> GetQuery<T,TResult>(
        IQueryable<T> query,
        ISpecification<T, TResult> spec)
    {
        if (spec == null)
            throw new ArgumentNullException(nameof(spec));

        // Filtering
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        // Sorting
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDesc != null)
        {
            query = query.OrderByDescending(spec.OrderByDesc);
        }

        // Projection (REQUIRED)
        var selector = spec.Select
            ?? throw new InvalidOperationException("Select expression cannot be null for projection specification.");

        var projectedQuery = query.Select(selector);

        // Distinct AFTER projection
        if (spec.IsDistinct)
        {
            projectedQuery = projectedQuery.Distinct();
        }

        return projectedQuery;
    }
}