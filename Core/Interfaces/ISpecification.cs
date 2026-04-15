using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces;
public interface ISpecification<T>
{
    public Expression<Func<T,bool>>? Criteria {get;}
    public Expression<Func<T,Object>>? OrderBy {get;}
    public Expression<Func<T,Object>>? OrderByDesc {get;}
    public bool IsDistinct {get;}
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    public Expression<Func<T, TResult>>? Select { get; }
}