using System;

namespace MongoDB.Extensions.AggregationFramework
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
