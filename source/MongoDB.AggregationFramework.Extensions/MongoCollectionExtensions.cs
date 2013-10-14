using MongoDB.Driver;
using System;

namespace MongoDB.AggregationFramework.Extensions
{
    public static class MongoCollectionExtensions
    {
        public static IPipeline<TClass> CreatePipeline<TClass>(this MongoCollection<TClass> collection)
        {
            return new Pipeline<TClass>(collection);
        }
    }
}
