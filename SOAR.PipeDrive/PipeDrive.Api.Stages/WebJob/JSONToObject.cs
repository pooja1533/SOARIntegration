using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SOARIntegration.PipeDrive.Api.Stages.WebJob
{
    public partial class JSONToObject
    {
        [JsonProperty("data")]
        public StageData[] Data { get; set; }
    }

    public partial class StageData
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("order_nr")]
        public string OrderNr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("active_flag")]
        public string ActiveFlag { get; set; }

        [JsonProperty("deal_probability")]
        public string DealProbability { get; set; }

        [JsonProperty("pipeline_id")]
        public string PipelineId { get; set; }

        [JsonProperty("rotten_flag")]
        public string RottenFlag { get; set; }

        [JsonProperty("rotten_days")]
        public string RottenDays { get; set; }

        [JsonProperty("add_time")]
        public DateTime AddTime { get; set; }

        [JsonProperty("update_time")]
        public DateTime UpdateTime { get; set; }

        [JsonProperty("pipeline_name")]
        public string PipelineName { get; set; }
    }
    
    public partial class JSONToObject
    {
        public static JSONToObject FromJson(string json) => JsonConvert.DeserializeObject<JSONToObject>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this JSONToObject self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

   
}
