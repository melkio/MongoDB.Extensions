using System;
using System.Linq.Expressions;

namespace MongoDB.AggregationFramework.Extensions
{
    public interface IPipeline<T>
    {
        IPipeline<T> AddMatch<T>(Expression<Func<T>> property, Expression<T> expression);
    }
}
