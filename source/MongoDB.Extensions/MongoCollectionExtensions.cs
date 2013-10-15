using MongoDB.Driver;
using System;

namespace MongoDB.Extensions
{
    public static class MongoCollectionExtensions
    {
        public static IPipeline<TClass> CreatePipeline<TClass>(this MongoCollection<TClass> collection)
        {
            return new Pipeline<TClass>(collection);
        }
    }
}
