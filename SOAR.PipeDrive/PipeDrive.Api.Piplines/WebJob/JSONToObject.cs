using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SOARIntegration.PipeDrive.Api.Pipelines.WebJob
{
    public partial class JSONToObject
    {
        [JsonProperty("data")]
        public PipelineData[] Data { get; set; }
    }
   
    public partial class PipelineData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url_title")]
        public string UrlTitle { get; set; }

        [JsonProperty("order_nr")]
        public int OrderNr { get; set; }

        [JsonProperty("active")]
        public string Active { get; set; }

        [JsonProperty("deal_probability")]
        public string DealProbability { get; set; }

        [JsonProperty("add_time")]
        public DateTime? AddTime { get; set; }

        [JsonProperty("update_time")]
        public DateTime? UpdateTime { get; set; }

        [JsonProperty("selected")]
        public string Selected { get; set; }
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
