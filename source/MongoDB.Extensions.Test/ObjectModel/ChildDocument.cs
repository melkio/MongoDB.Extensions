using System;

namespace MongoDB.Extensions.Test.ObjectModel
{
    public class ChildDocument
    {
        public String Name { get; set; }
        public LeafModel Leaf { get; set; }
    }
}
