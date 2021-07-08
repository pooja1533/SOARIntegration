using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Stages.Service;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model;

namespace SOARIntegration.PipeDrive.Api.Stages.WebJob
{
    public class StageModel
    {
        #region Private Fields
        private JSONToObject _stageList;
        #endregion

        #region CTOR
        public StageModel(JSONToObject stage)
        {
            _stageList = stage;
        }
        #endregion

        #region ProcessData
        public void ProcessData(IStagesService StagesService)
        {
            try
            {
                List<Stage> stageList = new List<Stage>();
                foreach (var stage in _stageList.Data)
                {
                    var objDeal = MapFields(stage);
                    stageList.Add(objDeal);
                }

                StagesService.InsertStages(stageList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Map Fields
        private Stage MapFields(StageData stage)
        {
            Stage objstage = new Stage();
            objstage.EntityId = stage.Id.ToString();
            objstage.Name = stage.Name;
            objstage.Order_Nr = stage.OrderNr;
            objstage.Active_Flag = stage.ActiveFlag;
            objstage.Deal_Probability = stage.DealProbability;
            objstage.Pipeline_Id = stage.PipelineId;
            objstage.Rotten_Flag = stage.RottenFlag;
            objstage.Rotten_Days = stage.RottenDays;
            objstage.Add_Time = stage.AddTime;
            objstage.Update_Time = stage.UpdateTime;
            objstage.Pipeline_Name = stage.PipelineName;

            return objstage;
        }

        #endregion
    }
}
