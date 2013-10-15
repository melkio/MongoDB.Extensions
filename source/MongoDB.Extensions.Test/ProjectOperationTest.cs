using MongoDB.Extensions.Test.ObjectModel;
using MongoDB.Bson.Serialization;
using NUnit.Framework;
using System;

namespace MongoDB.Extensions.Test
{
    [TestFixture]
    public class ProjectOperationTest
    {
        [Test]
        public void Apply()
        {
            BsonClassMap.RegisterClassMap<RootDocument>(m =>
            {
                m.MapIdProperty(d => d.Id);
                m.MapProperty(d => d.Value).SetElementName("v");
                m.MapProperty(d => d.Child).SetElementName("e");
            });

            BsonClassMap.RegisterClassMap<ChildDocument>(m => m.MapProperty(d => d.Name).SetElementName("n"));

            var operation = new ProjectOperation<RootDocument>();
            operation.Contains(d => d.Child.Name);
            operation.NotContains(d => d.Id);

            var document = operation.Apply();
            
            
        }
    }
}
