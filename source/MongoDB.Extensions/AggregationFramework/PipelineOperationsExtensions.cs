using MongoDB.Extensions.AggregationFramework.Operations;
using System;
using System.Linq.Expressions;

namespace MongoDB.Extensions.AggregationFramework
{
    public static class PipelineOperationsExtensions
    {
        public static IPipeline<TClass> Match<TClass>(this IPipeline<TClass> pipeline, Expression<Func<TClass, Boolean>> predicate)
        {
            var operation = new MatchOperation<TClass>(pipeline.Collection, predicate);
            pipeline.AddOperation(operation);
            
            return pipeline;
        }

        public static IPipeline<TClass> Project<TClass>(this IPipeline<TClass> pipeline, Action<IProjectOperation<TClass>> configuration)
        {
            var operation = new ProjectOperation<TClass>();
            configuration(operation);
            pipeline.AddOperation(operation);

            return pipeline;
        }
    }
}
