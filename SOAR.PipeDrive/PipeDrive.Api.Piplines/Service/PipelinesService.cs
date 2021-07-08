#region NameSpace
using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Pipelines.Repository;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Pipelines.Service
{
    public class PipelinesService : IPipelinesService
    {
		#region Private Fields
		private IRepository<Pipeline> _repository;
		#endregion

		#region CTOR
		public PipelinesService(IRepository<Pipeline> repository)
		{
			this._repository = repository;
		}
		#endregion

		#region InsertPipelines
		public void InsertPipelines(List<Pipeline> pipelines)
		{
			for (var count = 0; count < pipelines.Count; count++)
			{
				try
				{
					var pipeline = _repository.Get(pipelines[count].EntityId);
					if (pipeline == null)
					{
						_repository.Insert(pipelines[count]);
					}
					else
					{
						pipelines[count].Id = pipeline.Id;
						pipelines[count].Audit_Created = pipeline.Audit_Created;
						pipelines[count].Audit_Modified = pipeline.Audit_Modified;
						pipelines[count].Audit_Deleted = pipeline.Audit_Deleted;
						_repository.Update(pipelines[count]);
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
		} 
		#endregion
	}
}
