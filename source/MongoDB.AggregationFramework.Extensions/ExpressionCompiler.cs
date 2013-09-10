using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDB.AggregationFramework.Extensions
{
    public sealed class ExpressionCompiler<TClass> : IExpressionCompiler<TClass>
    {
        public String GetElementName<TMember>(Expression<Func<TClass, TMember>> expression)
        {
            var lambda = expression as LambdaExpression;
            var method = lambda.Body as MemberExpression;
            
            var list = new List<dynamic>();
            do
            {
                var map = new { PropertyName = method.Member.Name, PropertyType = method.Member.ReflectedType };
                list.Insert(0, map);

                method = method.Expression as MemberExpression;
            } while (method != null);

            var result = list.Aggregate<dynamic, String>("", (v, m) =>
                {
                    var map = BsonClassMap.LookupClassMap(m.PropertyType);
                    var member = map.GetMemberMap(m.PropertyName);

                    return String.Concat(v, ".", member.ElementName);
                });

            return result.Substring(1);
        }
    }
}
