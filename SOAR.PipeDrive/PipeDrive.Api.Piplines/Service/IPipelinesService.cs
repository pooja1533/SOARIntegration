using System.Collections.Generic;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Pipelines.Service
{
    public interface IPipelinesService
    {
        void InsertPipelines(List<Pipeline> pipelines);
    }
}
