using System.Linq.Expressions;
using System.Xml.XPath;
using Core.Interfaces;

namespace Core.Specifications;


public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification():this(null){}
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, Object>>? OrderBy {get; private set;}

    public Expression<Func<T, Object>>? OrderByDesc {get; private set;}

    public bool IsDistinct  {get; private set;}

    protected void AddOrderBy(Expression<Func<T, Object>> expression)
    {
        this.OrderBy = expression;
    }
    protected void AddOrderByDesc(Expression<Func<T, Object>> expression)
    {
        this.OrderByDesc = expression;
    }
    protected void ApplyDistinct()
    {
        this.IsDistinct = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification():this(null){}
    public Expression<Func<T, TResult>>? Select {get; private set;} 
    protected void AddSelect(Expression<Func<T, TResult>> selectExpression )
    {
        this.Select = selectExpression;
    }
}