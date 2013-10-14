using System;

namespace MongoDB.AggregationFramework.Extensions
{
    public enum PipelineOperations
    {
        Match,
        Project,
        Limit,
        Skip,
        Unwind,
        Group,
        Sort,
        GeoNear
    }
}
