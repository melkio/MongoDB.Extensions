using MongoDB.Extensions;
using MongoDB.Extensions.Test.ObjectModel;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDB.Extensions.Test
{
    [TestFixture]
    public class AcceptanceTest
    {
        private MongoCollection<RootDocument> _collection;

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
            //BsonClassMap.RegisterClassMap<RootDocument>(m =>
            //{
            //    m.MapIdProperty(d => d.Id);
            //    m.MapProperty(d => d.Value).SetElementName("v");
            //    m.MapProperty(d => d.Child).SetElementName("e");
            //});

            //BsonClassMap.RegisterClassMap<ChildDocument>(m => m.MapProperty(d => d.Name).SetElementName("n"));

            //Expression<Func<RootDocument,Boolean>> predicate = d => d.Value == 5 && d.Child.Name == "ale";

            //var queryable = _collection.AsQueryable<RootDocument>()
            //    //.Where(d => (d.Value > 5 && d.Value < 20) || (d.EmbeddedDocument.Name == "ale"))
            //    .Select(d => new { MyName = d.Child.Name, MyValue = d.Value });

            //var selectQuery = MongoQueryTranslator.Translate(queryable) as SelectQuery;
            //var query = selectQuery.BuildQuery();
            
            



            // Shows how pipeline could be created

            var result = _collection.CreatePipeline()
                .Match(d => d.Id == "Alessandro")
                .Project(c =>
                    {
                        c.Contains(d => d.Value);
                        c.NotContains(d => d.Id);
                    })
                .Execute();


            // _collection.Aggregate(pipeline);
        }
    }
}
