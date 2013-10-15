using MongoDB.Driver;

namespace MongoDB.Extensions.AggregationFramework
{
    public static class MongoCollectionExtensions
    {
        public static IPipeline<TClass> CreatePipeline<TClass>(this MongoCollection<TClass> collection)
        {
            return new Pipeline<TClass>(collection);
        }
    }
}
