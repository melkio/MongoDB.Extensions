using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDB.Extensions.AggregationFramework.Operations
{
    class MatchOperation<TClass> : IPipelineOperation<TClass>
    {
        private readonly MongoCollection<TClass> _collection;
        private readonly Expression<Func<TClass, Boolean>> _predicate;

        public PipelineOperations Type
        {
            get { return PipelineOperations.Match; }
        }

        public MatchOperation(MongoCollection<TClass> collection, Expression<Func<TClass, Boolean>> predicate)
        {
            _collection = collection;
            _predicate = predicate;
        }

        public BsonDocument Apply()
        {
            var queryable = _collection.AsQueryable<TClass>()
                .Where(_predicate)
                .Select(d => d);

            var selectQuery = MongoQueryTranslator.Translate(queryable) as SelectQuery;
            var query = selectQuery.BuildQuery();

            var document = new BsonDocument();
            document.Add("$match", query.ToBsonDocument());

            return document;
        }
    }
}
