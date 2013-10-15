using MongoDB.Bson;

namespace MongoDB.Extensions.AggregationFramework.ComponentModel
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
