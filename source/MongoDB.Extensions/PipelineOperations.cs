using System;

namespace MongoDB.Extensions
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
