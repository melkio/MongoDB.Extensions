using System;

namespace MongoDB.AggregationFramework.Extensions.Test.ObjectModel
{
    public class ChildDocument
    {
        public String Name { get; set; }
        public LeafModel Leaf { get; set; }
    }
}
