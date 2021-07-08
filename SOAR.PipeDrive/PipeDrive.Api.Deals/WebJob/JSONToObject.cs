using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SOARIntegration.PipeDrive.Api.Deals.WebJob
{
	public partial class JSONToObject
    {
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("data")]
		public DealData[] Data { get; set; }

		[JsonProperty("related_objects")]
		public RelatedObjects RelatedObjects { get; set; }
	}

	public partial class DealData
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("creator_user_id")]
		public CreatorUserId CreatorUserId { get; set; }

		[JsonProperty("user_id")]
		public CreatorUserId UserId { get; set; }

		[JsonProperty("person_id")]
		public PersonId PersonId { get; set; }

		[JsonProperty("org_id")]
		public Org OrgId { get; set; }

		[JsonProperty("stage_id")]
		public string StageId { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

		[JsonProperty("currency")]
		public string Currency { get; set; }

		[JsonProperty("add_time")]
		public DateTime? AddTime { get; set; }

		[JsonProperty("update_time")]
		public DateTime? UpdateTime { get; set; }

		[JsonProperty("stage_change_time")]
		public DateTime? StageChangeTime { get; set; }

		[JsonProperty("active")]
		public string Active { get; set; }

		[JsonProperty("deleted")]
		public string Deleted { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("probability")]
		public string Probability { get; set; }

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

		[JsonProperty("lost_reason")]
		public string LostReason { get; set; }

		[JsonProperty("visible_to")]
		[JsonConverter(typeof(ParseStringConverter))]
		public int? VisibleTo { get; set; }

		[JsonProperty("close_time")]
		public DateTime? CloseTime { get; set; }

		[JsonProperty("pipeline_id")]
		public int? PipelineId { get; set; }

		[JsonProperty("won_time")]
		public DateTime? WonTime { get; set; }

		[JsonProperty("first_won_time")]
		public DateTime? FirstWonTime { get; set; }

		[JsonProperty("lost_time")]
		public DateTime? LostTime { get; set; }

		[JsonProperty("products_count")]
		public int? ProductsCount { get; set; }

		[JsonProperty("files_count")]
		public int? FilesCount { get; set; }

		[JsonProperty("notes_count")]
		public int? NotesCount { get; set; }

		[JsonProperty("followers_count")]
		public int? FollowersCount { get; set; }

		[JsonProperty("email_messages_count")]
		public int? EmailMessagesCount { get; set; }

		[JsonProperty("activities_count")]
		public int? ActivitiesCount { get; set; }

		[JsonProperty("done_activities_count")]
		public int? DoneActivitiesCount { get; set; }

		[JsonProperty("undone_activities_count")]
		public int? UndoneActivitiesCount { get; set; }

		[JsonProperty("reference_activities_count")]
		public int? ReferenceActivitiesCount { get; set; }

		[JsonProperty("participants_count")]
		public int? ParticipantsCount { get; set; }

		[JsonProperty("expected_close_date")]
		public DateTime? ExpectedCloseDate { get; set; }

		[JsonProperty("last_incoming_mail_time")]
		public DateTime? LastIncomingMailTime { get; set; }

		[JsonProperty("last_outgoing_mail_time")]
		public DateTime? LastOutgoingMailTime { get; set; }

		//[JsonProperty("a63ea572448dfe5c864aed9c988f2cf02887c4b2")]
		//public string A63Ea572448Dfe5C864Aed9C988F2Cf02887C4B2 { get; set; }

		//[JsonProperty("5cfdea5b20d4d49a4c22153b0b066a382c996152")]
		//public string The5Cfdea5B20D4D49A4C22153B0B066A382C996152 { get; set; }

		//[JsonProperty("41b5fec30ab7aec3eb3213a2a3fc8dada2b7d131")]
		//public string The41B5Fec30Ab7Aec3Eb3213A2A3Fc8Dada2B7D131 { get; set; }

		//[JsonProperty("3b90fc1479d2fa465f940b7e4a4189363416005d")]
		//public string The3B90Fc1479D2Fa465F940B7E4A4189363416005D { get; set; }

		//[JsonProperty("eb684f437aa52a89df7de5a2676fac54c81cbc37")]
		//[JsonConverter(typeof(ParseStringConverter))]
		//public long? Eb684F437Aa52A89Df7De5A2676Fac54C81Cbc37 { get; set; }

		//[JsonProperty("052f1fadf4715a6df953add1acf8a4a42f373077")]
		//[JsonConverter(typeof(ParseStringConverter))]
		//public long? The052F1Fadf4715A6Df953Add1Acf8A4A42F373077 { get; set; }

		[JsonProperty("stage_order_nr")]
		public int? StageOrderNr { get; set; }

		[JsonProperty("person_name")]
		public string PersonName { get; set; }

		[JsonProperty("org_name")]
		public string OrgName { get; set; }

		[JsonProperty("next_activity_subject")]
		public string NextActivitySubject { get; set; }

		[JsonProperty("next_activity_type")]
		public string NextActivityType { get; set; }

		[JsonProperty("next_activity_duration")]
		public string NextActivityDuration { get; set; }

		[JsonProperty("next_activity_note")]
		public string NextActivityNote { get; set; }

		[JsonProperty("formatted_value")]
		public string FormattedValue { get; set; }

		[JsonProperty("weighted_value")]
		public string WeightedValue { get; set; }

		[JsonProperty("formatted_weighted_value")]
		public string FormattedWeightedValue { get; set; }

		[JsonProperty("weighted_value_currency")]
		public string WeightedValueCurrency { get; set; }

		[JsonProperty("rotten_time")]
		public DateTime? RottenTime { get; set; }

		[JsonProperty("owner_name")]
		public string OwnerName { get; set; }

		[JsonProperty("cc_email")]
		public string CcEmail { get; set; }

		[JsonProperty("org_hidden")]
		public string OrgHidden { get; set; }

		[JsonProperty("person_hidden")]
		public string PersonHidden { get; set; }
	}

	public partial class CreatorUserId
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

	public partial class Org
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("people_count")]
		public long PeopleCount { get; set; }

		[JsonProperty("owner_id")]
		public long OwnerId { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("cc_email")]
		public string CcEmail { get; set; }

		[JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
		public long? Value { get; set; }

		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public long? Id { get; set; }
	}

	public partial class PersonId
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("email")]
		public EmailElement[] Email { get; set; }

		[JsonProperty("phone")]
		public EmailElement[] Phone { get; set; }

		[JsonProperty("value")]
		public long Value { get; set; }
	}

	public partial class EmailElement
	{
		[JsonProperty("label", NullValueHandling = NullValueHandling.Ignore)]
		public string Label { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }

		[JsonProperty("primary")]
		public bool Primary { get; set; }
	}

	public partial class RelatedObjects
	{
		[JsonProperty("user")]
		public Dictionary<string, CreatorUserId> User { get; set; }

		[JsonProperty("organization")]
		public Dictionary<string, Org> Organization { get; set; }

		[JsonProperty("person")]
		public Dictionary<string, Person> Person { get; set; }
	}

	public partial class Person
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("email")]
		public EmailElement[] Email { get; set; }

		[JsonProperty("phone")]
		public EmailElement[] Phone { get; set; }
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
			int i;
			if (t == typeof(Int32?))
			{
				return Int32.TryParse(value, out i) ? (int?)i : null;

			}
			else if (Int64.TryParse(value, out l))
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