using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MongoDB.Extensions.AggregationFramework.ComponentModel.Operations
{
    public interface IProjectOperation<TClass> : IPipelineOperation<TClass>
    {
        void Contains<TMember>(Expression<Func<TClass, TMember>> project);
        void Contains<TMember>(String propertyName, Expression<Func<TClass, TMember>> project);
        void NotContains<TMember>(Expression<Func<TClass, TMember>> project);
    }
}
