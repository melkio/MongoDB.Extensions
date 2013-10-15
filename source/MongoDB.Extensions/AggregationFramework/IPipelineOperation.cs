using MongoDB.Bson;

namespace MongoDB.Extensions.AggregationFramework
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
