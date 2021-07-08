#region NameSpace
using System;
using System.Collections.Generic;
using SOARIntegration.PipeDrive.Api.Pipelines.Service;
using SOARIntegration.SOAR.Shared.PipeDrive.Common.Model; 
#endregion

namespace SOARIntegration.PipeDrive.Api.Pipelines.WebJob
{
    public class PipelineModel
    {
        #region Private Fields
        private JSONToObject _pipelineList;
        #endregion

        #region CTOR
        public PipelineModel(JSONToObject pipeline)
        {
            _pipelineList = pipeline;
        }
        #endregion

        #region ProcessData
        public void ProcessData(IPipelinesService pipelinesService)
        {
            try
            {
                List<Pipeline> pipelineList = new List<Pipeline>();
                foreach (var pipeline in _pipelineList.Data)
                {
                    var objDeal = MapFields(pipeline);
                    pipelineList.Add(objDeal);
                }

                pipelinesService.InsertPipelines(pipelineList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Map Fields
        private Pipeline MapFields(PipelineData pipeline)
        {
            Pipeline objPipeline = new Pipeline();
            objPipeline.EntityId = pipeline.Id.ToString();
            objPipeline.Name = pipeline.Name;
            objPipeline.URL_Title = pipeline.UrlTitle;
            objPipeline.Order_Nr = pipeline.OrderNr;
            objPipeline.Active = pipeline.Active;
            objPipeline.Deal_Probability = pipeline.DealProbability;
            objPipeline.Add_Time = pipeline.AddTime;
            objPipeline.Update_Time = pipeline.UpdateTime;
            objPipeline.Selected = pipeline.Selected;

            return objPipeline;
        }

        #endregion
    }
}
