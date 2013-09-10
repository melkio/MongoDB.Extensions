using System;
using System.Linq.Expressions;

namespace MongoDB.AggregationFramework.Extensions
{
    public interface IExpressionCompiler<TClass>
    {
        String GetElementName<TMember>(Expression<Func<TClass, TMember>> expression);
    }
}
