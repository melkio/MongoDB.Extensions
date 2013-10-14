using MongoDB.AggregationFramework.Extensions.Operations;
using MongoDB.AggregationFramework.Extensions.Test.ObjectModel;
using MongoDB.Driver;
using NUnit.Framework;
using System;

namespace MongoDB.AggregationFramework.Extensions.Test
{
    [TestFixture]
    public class MatchOperationTest
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
        public void Apply()
        {
            var operation = new MatchOperation<RootDocument>(_collection, d => d.Value == 10 && d.Child.Name == "Alessandro");
            var document = operation.Apply();
        }
    }
}
