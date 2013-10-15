using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.Extensions.AggregationFramework.ComponentModel
{
    public interface IPipeline<TClass> 
    {
        MongoCollection<TClass> Collection { get; }
        void AddOperation(IPipelineOperation<TClass> operation);
        AggregateResult Execute();
    }
}
