using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class ProductBrandSpecification : BaseSpecification<Product, string>
{

    public ProductBrandSpecification()
    {
        AddSelect(x => x.Brand);
        ApplyDistinct();
    }
}