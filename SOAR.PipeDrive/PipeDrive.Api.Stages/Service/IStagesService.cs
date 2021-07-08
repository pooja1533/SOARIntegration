using System.Collections.Generic;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Stages.Service
{
    public interface IStagesService
    {
        void InsertStages(List<Stage> Stages);
    }
}
