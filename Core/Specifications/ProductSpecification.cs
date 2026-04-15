using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string? brand, string? type, string? sort)
        : base(BuildCriteria(brand, type))
    {
        ApplySorting(sort);
    }

    private static Expression<Func<Product, bool>> BuildCriteria(string? brand, string? type)
    {
        return x =>
            (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
            (string.IsNullOrWhiteSpace(type) || x.Type == type);
    }

    private void ApplySorting(string? sort)
    {
        if (string.IsNullOrWhiteSpace(sort))
        {
            AddOrderBy(x => x.Name);
            return;
        }

        switch (sort.ToLower())
        {
            case "priceasc":
                AddOrderBy(x => x.Price);
                break;

            case "pricedesc":
                AddOrderByDesc(x => x.Price);
                break;

            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}