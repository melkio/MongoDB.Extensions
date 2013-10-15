using System;

namespace MongoDB.Extensions
{
    public interface IPipelineBuilder<TClass>
    {
        IPipeline<TClass> Then { get; }
    }

    class PipelineBuilder<TClass> : IPipelineBuilder<TClass>
    {
        public IPipeline<TClass> Then { get; private set; }

        public PipelineBuilder(IPipeline<TClass> pipeline)
        {
            Then = pipeline;
        }
    }

}
