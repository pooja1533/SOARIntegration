using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SOARIntegration.PipeDrive.Api.Products.WebJob
{
	public partial class JSONToObject
	{
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("data")]
		public ProductData[] Data { get; set; }

		[JsonProperty("additional_data")]
		public AdditionalData AdditionalData { get; set; }

		[JsonProperty("related_objects")]
		public RelatedObjects RelatedObjects { get; set; }
	}

	public partial class AdditionalData
	{
		[JsonProperty("pagination")]
		public Pagination Pagination { get; set; }
	}

	public partial class Pagination
	{
		[JsonProperty("start")]
		public long Start { get; set; }

		[JsonProperty("limit")]
		public long Limit { get; set; }

		[JsonProperty("more_items_in_collection")]
		public bool MoreItemsInCollection { get; set; }
	}

	public partial class ProductData
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("code")]
		public string Code { get; set; }

		[JsonProperty("unit")]
		public string Unit { get; set; }

		[JsonProperty("tax")]
		public int Tax { get; set; }

		[JsonProperty("active_flag")]
		public string ActiveFlag { get; set; }

		[JsonProperty("selectable")]
		public string Selectable { get; set; }

		[JsonProperty("first_char")]
		public string FirstChar { get; set; }

		[JsonProperty("visible_to")]
		public string VisibleTo { get; set; }

		[JsonProperty("owner_id")]
		public OwnerId OwnerId { get; set; }

		[JsonProperty("files_count")]
		public int? FilesCount { get; set; }

		[JsonProperty("followers_count")]
		public int FollowersCount { get; set; }

		[JsonProperty("add_time")]
		public DateTime AddTime { get; set; }

		[JsonProperty("update_time")]
		public DateTime UpdateTime { get; set; }		
	}

	public partial class OwnerId
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("has_pic")]
		public bool HasPic { get; set; }

		[JsonProperty("pic_hash")]
		public string PicHash { get; set; }

		[JsonProperty("active_flag")]
		public bool ActiveFlag { get; set; }

		[JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
		public long? Value { get; set; }
	}

	public partial class Price
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("product_id")]
		public long ProductId { get; set; }

		[JsonProperty("price")]
		public long PricePrice { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("cost")]
		public long Cost { get; set; }

		[JsonProperty("overhead_cost")]
		public long OverheadCost { get; set; }
	}

	public partial class RelatedObjects
	{
		[JsonProperty("user")]
		public Dictionary<string, OwnerId> User { get; set; }
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

	internal class ParseStringConverter : JsonConverter
	{
		public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

		public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null) return null;
			var value = serializer.Deserialize<string>(reader);
			long l;
			if (Int64.TryParse(value, out l))
			{
				return l;
			}
			throw new Exception("Cannot unmarshal type long");
		}

		public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
		{
			if (untypedValue == null)
			{
				serializer.Serialize(writer, null);
				return;
			}
			var value = (long)untypedValue;
			serializer.Serialize(writer, value.ToString());
			return;
		}

		public static readonly ParseStringConverter Singleton = new ParseStringConverter();
	}
}
