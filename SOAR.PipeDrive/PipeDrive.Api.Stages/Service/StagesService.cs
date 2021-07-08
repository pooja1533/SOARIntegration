#region NameSpace
using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Stages.Repository;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Stages.Service
{
    public class StagesService : IStagesService
    {
		#region Private Fields
		private IRepository<Stage> _repository;
		#endregion

		#region CTOR
		public StagesService(IRepository<Stage> repository)
		{
			this._repository = repository;
		}
		#endregion

		#region InsertStages
		public void InsertStages(List<Stage> stages)
		{
			for (var count = 0; count < stages.Count; count++)
			{
				try
				{
					var stage = _repository.Get(stages[count].EntityId);
					if (stage == null)
					{
						_repository.Insert(stages[count]);
					}
					else
					{
						stages[count].Id = stage.Id;
						stages[count].Audit_Created = stage.Audit_Created;
						stages[count].Audit_Modified = stage.Audit_Modified;
						stages[count].Audit_Deleted = stage.Audit_Deleted;
						_repository.Update(stages[count]);
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
