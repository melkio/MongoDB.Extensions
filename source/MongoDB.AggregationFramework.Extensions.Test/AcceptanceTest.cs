using MongoDB.AggregationFramework.Extensions.Test.ObjectModel;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDB.AggregationFramework.Extensions.Test
{
    [TestFixture]
    public class AcceptanceTest
    {
        private MongoCollection _collection;

        [SetUp]
        public void SetUp()
        {
            var url = new MongoUrl("mongodb://localhost/aggregate");

            var client = new MongoClient(url.Url);
            var server = client.GetServer();
            var database = server.GetDatabase(url.DatabaseName);
            _collection = database.GetCollection<RootDocument>("fixtures");
        }

        [Test]
        public void DefiningFlows()
        {
            BsonClassMap.RegisterClassMap<RootDocument>(m =>
            {
                m.MapIdProperty(d => d.Id);
                m.MapProperty(d => d.Value).SetElementName("v");
                m.MapProperty(d => d.Child).SetElementName("e");
            });

            BsonClassMap.RegisterClassMap<ChildDocument>(m => m.MapProperty(d => d.Name).SetElementName("n"));

            Expression<Func<RootDocument,Boolean>> predicate = d => d.Value == 5 && d.Child.Name == "ale";

            var queryable = _collection.AsQueryable<RootDocument>()
                //.Where(d => (d.Value > 5 && d.Value < 20) || (d.EmbeddedDocument.Name == "ale"))
                .Select(d => new { MyName = d.Child.Name, MyValue = d.Value });

            var selectQuery = MongoQueryTranslator.Translate(queryable) as SelectQuery;
            var query = selectQuery.BuildQuery();
            
            



            // Shows how pipeline could be created

            //var pipeline = Pipeline.For<Document>()
            //     .Match(d => d.Value).IsGreaterThan(10)
            //         .And(d => d.EmbeddeDocument.Value).Is("test")
            //     .Then.Project(d => d.Value)
            //         .AndNot(d => d.Id);


            // _collection.Aggregate(pipeline);
        }
    }
}
