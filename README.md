MongoDB.Extensions
==================

Using aggregation framework with C# seems very verbose.
MongoDB.Extensions contains a fluent interface around pipeline operations to build and execute aggregation.

You can try it, installing nuget package into your solution:  
**Install-Package MongoDB.Extensions**    

An easy sample, about how to use package API:  

    var result = _collection.CreatePipeline()
                .Match(d => d.Value >= 10)
                .Project(c =>
                    {
                        c.Contains(d => d.Value);
                        c.Contains("temp", d => d.Child.Leaf.Value);
                        c.NotContains(d => d.Id);
                    })
                .Limit(3)
                .Execute();
