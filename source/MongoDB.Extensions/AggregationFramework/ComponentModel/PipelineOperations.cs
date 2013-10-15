using System;

namespace MongoDB.Extensions.AggregationFramework.ComponentModel
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
