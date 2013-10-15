using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MongoDB.Extensions.AggregationFramework
{
    public interface IProjectOperation<TClass> : IPipelineOperation<TClass>
    {
        void Contains<TMember>(Expression<Func<TClass, TMember>> project);
        void NotContains<TMember>(Expression<Func<TClass, TMember>> project);
    }

    class ProjectOperation<TClass> : IProjectOperation<TClass>
    {
        private readonly BsonDocument _operation;

        public PipelineOperations Type
        {
            get { return PipelineOperations.Project; }
        }

        public ProjectOperation()
        {
            _operation = new BsonDocument();
        }

        public void Contains<TMember>(Expression<Func<TClass, TMember>> project)
        {
            var element = ComputeElementName(project);
            _operation.Add(element, 1);
        }

        public void NotContains<TMember>(Expression<Func<TClass, TMember>> project)
        {
            var map = (BsonClassMap<TClass>)BsonClassMap.LookupClassMap(typeof(TClass));
            var element = map.GetMemberMap(project).ElementName;
            _operation.Add(element, 0);
        }

        public BsonDocument Apply()
        {
            var document = new BsonDocument();
            document.Add("$project", _operation);

            return document;
        }

        private String ComputeElementName<TMember>(Expression<Func<TClass, TMember>> project)
        {
            var dictionary = new Dictionary<Type, String>();

            var expression = project.Body as MemberExpression;
            while (expression != null)
            {
                var type = expression.Member.DeclaringType;
                var name = expression.Member.Name;
                dictionary.Add(type, name);

                expression = expression.Expression as MemberExpression;
            }

            var builder = new StringBuilder();
            foreach (var pair in dictionary.Reverse())
            {
                var map = BsonClassMap.LookupClassMap(pair.Key);
                var element = map.GetMemberMap(pair.Value).ElementName;

                builder.AppendFormat("{0}.", element);
            }

            var result = builder.ToString();
            return result.Substring(0, result.Length - 1);
        }
    }
}
