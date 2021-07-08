using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SOARIntegration.PipeDrive.Api.Organizations.WebJob
{
	public partial class JSONToObject
	{
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("data")]
		public OrganizationData[] Data { get; set; }

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

		[JsonProperty("next_start")]
		public long NextStart { get; set; }
	}

	public partial class OrganizationData
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("company_id")]
		public long CompanyId { get; set; }

		[JsonProperty("owner_id")]
		public OwnerId OwnerId { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("open_deals_count")]
		public int OpenDealsCount { get; set; }

		[JsonProperty("related_open_deals_count")]
		public int RelatedOpenDealsCount { get; set; }

		[JsonProperty("closed_deals_count")]
		public int ClosedDealsCount { get; set; }

		[JsonProperty("related_closed_deals_count")]
		public int RelatedClosedDealsCount { get; set; }

		[JsonProperty("email_messages_count")]
		public int EmailMessagesCount { get; set; }

		[JsonProperty("people_count")]
		public int PeopleCount { get; set; }

		[JsonProperty("activities_count")]
		public int ActivitiesCount { get; set; }

		[JsonProperty("done_activities_count")]
		public int DoneActivitiesCount { get; set; }

		[JsonProperty("undone_activities_count")]
		public int UndoneActivitiesCount { get; set; }

		[JsonProperty("reference_activities_count")]
		public int ReferenceActivitiesCount { get; set; }

		[JsonProperty("files_count")]
		public int FilesCount { get; set; }

		[JsonProperty("notes_count")]
		public int NotesCount { get; set; }

		[JsonProperty("followers_count")]
		public int FollowersCount { get; set; }

		[JsonProperty("won_deals_count")]
		public int WonDealsCount { get; set; }

		[JsonProperty("related_won_deals_count")]
		public int RelatedWonDealsCount { get; set; }

		[JsonProperty("lost_deals_count")]
		public int LostDealsCount { get; set; }

		[JsonProperty("related_lost_deals_count")]
		public int RelatedLostDealsCount { get; set; }

		[JsonProperty("active_flag")]
		public string ActiveFlag { get; set; }

		[JsonProperty("category_id")]
		public string CategoryId { get; set; }

		[JsonProperty("picture_id")]
		public string PictureId { get; set; }

		[JsonProperty("country_code")]
		public string CountryCode { get; set; }

		[JsonProperty("first_char")]
		public string FirstChar { get; set; }

		[JsonProperty("update_time")]
		public DateTime? UpdateTime { get; set; }

		[JsonProperty("add_time")]
		public DateTime? AddTime { get; set; }

		[JsonProperty("visible_to")]
		public string VisibleTo { get; set; }

		[JsonProperty("next_activity_date")]
		public DateTime? NextActivityDate { get; set; }

		[JsonProperty("next_activity_time")]
		public DateTime? NextActivityTime { get; set; }

		[JsonProperty("next_activity_id")]
		public string NextActivityId { get; set; }

		[JsonProperty("last_activity_id")]
		public string LastActivityId { get; set; }

		[JsonProperty("last_activity_date")]
		public DateTime? LastActivityDate { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("address_subpremise")]
		public string AddressSubpremise { get; set; }

		[JsonProperty("address_street_number")]
		public string AddressStreetNumber { get; set; }

		[JsonProperty("address_route")]
		public string AddressRoute { get; set; }

		[JsonProperty("address_sublocality")]
		public string AddressSublocality { get; set; }

		[JsonProperty("address_locality")]
		public string AddressLocality { get; set; }

		[JsonProperty("address_admin_area_level_1")]
		public string AddressAdminAreaLevel1 { get; set; }

		[JsonProperty("address_admin_area_level_2")]
		public string AddressAdminAreaLevel2 { get; set; }

		[JsonProperty("address_country")]
		public string AddressCountry { get; set; }

		[JsonProperty("address_postal_code")]
		public string AddressPostalCode { get; set; }

		[JsonProperty("address_formatted_address")]
		public string AddressFormattedAddress { get; set; }

		[JsonProperty("label")]
		public object Label { get; set; }

		[JsonProperty("owner_name")]
		public string OwnerName { get; set; }

		[JsonProperty("cc_email")]
		public string CcEmail { get; set; }
	}

	public partial class OwnerId
	{
		[JsonProperty("id")]
		public long Id { get; set; }

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

	public partial class RelatedObjects
	{
		[JsonProperty("user")]
		public User User { get; set; }
	}

	public partial class User
	{
		[JsonProperty("2290815")]
		public OwnerId The2290815 { get; set; }
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
