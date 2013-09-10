using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System;

namespace MongoDB.AggregationFramework.Extensions.Test
{
    [TestFixture]
    public class AcceptanceTest
    {
        private MongoCollection _collection;

        [Test]
        public void DefiningFlows()
        {
            // Shows how pipeline could be created

            //var pipeline = Pipeline.For<Document>()
            //     .Match(d => d.Value).IsGreaterThan(10)
            //         .And(d => d.EmbeddeDocument.Value).Is("test")
            //     .Then.Project(d => d.Value)
            //         .AndNot(d => d.Id);


            // _collection.Aggregate(pipeline);
        }

        //public class Document
        //{
        //    public String Id { get; set; }
        //    public Int32 Value { get; set; }
        //    public Nested EmbeddedDocument { get; set; }

        //    public class Nested
        //    {
        //        public String Value { get; set; }
        //    }
        //}
    }
}
