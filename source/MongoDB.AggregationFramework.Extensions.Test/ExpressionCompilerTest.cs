//using MongoDB.AggregationFramework.Extensions.Test.ObjectModel;
//using MongoDB.Bson.Serialization;
//using MongoDB.Driver.Linq;
//using NUnit.Framework;

//namespace MongoDB.AggregationFramework.Extensions.Test
//{
//    [TestFixture]
//    public class ExpressionCompilerTest
//    {
//        [TestFixtureSetUp]
//        public void SetUp()
//        {
//            BsonClassMap.RegisterClassMap<RootDocument>(m =>
//                {
//                    m.MapIdProperty(d => d.Id);
//                    m.MapProperty(d => d.Value).SetElementName("v");
//                    m.MapProperty(d => d.Child).SetElementName("e");
//                });

//            BsonClassMap.RegisterClassMap<ChildDocument>(m =>
//                {
//                    m.MapProperty(d => d.Name).SetElementName("n");
//                    m.MapProperty(d => d.Leaf).SetElementName("l");
//                });

//            BsonClassMap.RegisterClassMap<LeafModel>(m => m.MapProperty(d => d.Value).SetElementName("date"));
//        }

//        [Test]
//        public void GetElementName_MainPropertyIsMappedToMongoDbElement_ShouldReturnElementName()
//        {
//            var compiler = new ExpressionCompiler<RootDocument>();
//            var element = compiler.GetElementName(d => d.Value);

//            Assert.AreEqual("v", element);
//        }

//        [Test]
//        public void GetElementName_NestedPropertyIsMappedToMongoDbElement_ShouldReturnElementName()
//        {
//            var compiler = new ExpressionCompiler<RootDocument>();
//            var element = compiler.GetElementName(d => d.Child.Leaf.Value);

//            Assert.AreEqual("e.l.date", element);
//        }
//    }
//}
