using MongoDB.Bson;
using System;

namespace MongoDB.AggregationFramework.Extensions
{
    public interface IPipelineOperation
    {
        PipelineOperations Type { get; }
        BsonDocument Apply();
    }

    public interface IPipelineOperation<TClass> : IPipelineOperation
    { 

    }
}
