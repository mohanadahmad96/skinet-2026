using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;


public class ProductTypesSpecification : BaseSpecification<Product, string>
{

    public ProductTypesSpecification()
    {
        AddSelect(x => x.Type);
        ApplyDistinct();
    }
}
