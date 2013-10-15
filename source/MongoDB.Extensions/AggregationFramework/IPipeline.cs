using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MongoDB.Extensions.AggregationFramework
{
    public interface IPipeline<TClass> 
    {
        MongoCollection<TClass> Collection { get; }
        void AddOperation(IPipelineOperation<TClass> operation);
        AggregateResult Execute();
    }

    class Pipeline<TClass> : IPipeline<TClass>
    {
        private readonly IList<IPipelineOperation> _operations;

        public MongoCollection<TClass> Collection { get; private set; }

        public Pipeline(MongoCollection<TClass> collection)
        {
            _operations = new List<IPipelineOperation>();
            Collection = collection;
        }

        public void AddOperation(IPipelineOperation<TClass> operation)
        {
            if (_operations.Any(o => o.Type == operation.Type))
            {
                var message = String.Format("Already exists an operation of type: {0}", operation.Type);
                throw new InvalidOperationException(message);
            }

            _operations.Add(operation);
        }

        public AggregateResult Execute()
        {
            var operations = _operations.Select(o => o.Apply());
            return Collection.Aggregate(operations);
        }

    }
}
