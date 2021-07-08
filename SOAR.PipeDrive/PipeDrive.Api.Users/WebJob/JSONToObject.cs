using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SOARIntegration.PipeDrive.Api.Users.WebJob
{
    public partial class JSONToObject
    {
		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("data")]
		public UserData[] Data { get; set; }

		[JsonProperty("additional_data")]
		public AdditionalData AdditionalData { get; set; }
	}

	public partial class AdditionalData
	{
		[JsonProperty("company_id")]
		public long CompanyId { get; set; }
	}

	public partial class UserData
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("default_currency")]
		public string DefaultCurrency { get; set; }

		[JsonProperty("locale")]
		public string Locale { get; set; }

		[JsonProperty("lang")]
		public string Lang { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("activated")]
		public string Activated { get; set; }

		[JsonProperty("last_login")]
		public string LastLogin { get; set; }

		[JsonProperty("created")]
		public DateTime Created { get; set; }

		[JsonProperty("modified")]
		public DateTime Modified { get; set; }

		[JsonProperty("signup_flow_variation")]
		public string SignupFlowVariation { get; set; }

		[JsonProperty("has_created_company")]
		public string HasCreatedCompany { get; set; }

		[JsonProperty("is_admin")]
		public string IsAdmin { get; set; }

		[JsonProperty("timezone_name")]
		public string TimezoneName { get; set; }

		[JsonProperty("timezone_offset")]
		public string TimezoneOffset { get; set; }

		[JsonProperty("active_flag")]
		public string ActiveFlag { get; set; }

		[JsonProperty("role_id")]
		public string RoleId { get; set; }

		[JsonProperty("icon_url")]
		public string IconUrl { get; set; }

		[JsonProperty("is_you")]
		public string IsYou { get; set; }
	}

	//public enum DefaultCurrency { Zar };

	//public enum Locale { EnUs, EnZa };

	//public enum SignupFlowVariation { Invite, ShortForm };

	//public enum TimezoneName { AfricaJohannesburg, AfricaKampala };

	//public enum TimezoneOffset { The0200, The0300 };

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
				//DefaultCurrencyConverter.Singleton,
				//LocaleConverter.Singleton,
				//SignupFlowVariationConverter.Singleton,
				//TimezoneNameConverter.Singleton,
				//TimezoneOffsetConverter.Singleton,
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}

	//internal class DefaultCurrencyConverter : JsonConverter
	//{
	//	public override bool CanConvert(Type t) => t == typeof(DefaultCurrency) || t == typeof(DefaultCurrency?);

	//	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	//	{
	//		if (reader.TokenType == JsonToken.Null) return null;
	//		var value = serializer.Deserialize<string>(reader);
	//		if (value == "ZAR")
	//		{
	//			return DefaultCurrency.Zar;
	//		}
	//		throw new Exception("Cannot unmarshal type DefaultCurrency");
	//	}

	//	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	//	{
	//		if (untypedValue == null)
	//		{
	//			serializer.Serialize(writer, null);
	//			return;
	//		}
	//		var value = (DefaultCurrency)untypedValue;
	//		if (value == DefaultCurrency.Zar)
	//		{
	//			serializer.Serialize(writer, "ZAR");
	//			return;
	//		}
	//		throw new Exception("Cannot marshal type DefaultCurrency");
	//	}

	//	public static readonly DefaultCurrencyConverter Singleton = new DefaultCurrencyConverter();
	//}

	//internal class LocaleConverter : JsonConverter
	//{
	//	public override bool CanConvert(Type t) => t == typeof(Locale) || t == typeof(Locale?);

	//	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	//	{
	//		if (reader.TokenType == JsonToken.Null) return null;
	//		var value = serializer.Deserialize<string>(reader);
	//		switch (value)
	//		{
	//			case "en_US":
	//				return Locale.EnUs;
	//			case "en_ZA":
	//				return Locale.EnZa;
	//		}
	//		throw new Exception("Cannot unmarshal type Locale");
	//	}

	//	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	//	{
	//		if (untypedValue == null)
	//		{
	//			serializer.Serialize(writer, null);
	//			return;
	//		}
	//		var value = (Locale)untypedValue;
	//		switch (value)
	//		{
	//			case Locale.EnUs:
	//				serializer.Serialize(writer, "en_US");
	//				return;
	//			case Locale.EnZa:
	//				serializer.Serialize(writer, "en_ZA");
	//				return;
	//		}
	//		throw new Exception("Cannot marshal type Locale");
	//	}

	//	public static readonly LocaleConverter Singleton = new LocaleConverter();
	//}

	//internal class SignupFlowVariationConverter : JsonConverter
	//{
	//	public override bool CanConvert(Type t) => t == typeof(SignupFlowVariation) || t == typeof(SignupFlowVariation?);

	//	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	//	{
	//		if (reader.TokenType == JsonToken.Null) return null;
	//		var value = serializer.Deserialize<string>(reader);
	//		switch (value)
	//		{
	//			case "invite":
	//				return SignupFlowVariation.Invite;
	//			case "short_form":
	//				return SignupFlowVariation.ShortForm;
	//		}
	//		throw new Exception("Cannot unmarshal type SignupFlowVariation");
	//	}

	//	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	//	{
	//		if (untypedValue == null)
	//		{
	//			serializer.Serialize(writer, null);
	//			return;
	//		}
	//		var value = (SignupFlowVariation)untypedValue;
	//		switch (value)
	//		{
	//			case SignupFlowVariation.Invite:
	//				serializer.Serialize(writer, "invite");
	//				return;
	//			case SignupFlowVariation.ShortForm:
	//				serializer.Serialize(writer, "short_form");
	//				return;
	//		}
	//		throw new Exception("Cannot marshal type SignupFlowVariation");
	//	}

	//	public static readonly SignupFlowVariationConverter Singleton = new SignupFlowVariationConverter();
	//}

	//internal class TimezoneNameConverter : JsonConverter
	//{
	//	public override bool CanConvert(Type t) => t == typeof(TimezoneName) || t == typeof(TimezoneName?);

	//	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	//	{
	//		if (reader.TokenType == JsonToken.Null) return null;
	//		var value = serializer.Deserialize<string>(reader);
	//		switch (value)
	//		{
	//			case "Africa/Johannesburg":
	//				return TimezoneName.AfricaJohannesburg;
	//			case "Africa/Kampala":
	//				return TimezoneName.AfricaKampala;
	//		}
	//		throw new Exception("Cannot unmarshal type TimezoneName");
	//	}

	//	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	//	{
	//		if (untypedValue == null)
	//		{
	//			serializer.Serialize(writer, null);
	//			return;
	//		}
	//		var value = (TimezoneName)untypedValue;
	//		switch (value)
	//		{
	//			case TimezoneName.AfricaJohannesburg:
	//				serializer.Serialize(writer, "Africa/Johannesburg");
	//				return;
	//			case TimezoneName.AfricaKampala:
	//				serializer.Serialize(writer, "Africa/Kampala");
	//				return;
	//		}
	//		throw new Exception("Cannot marshal type TimezoneName");
	//	}

	//	public static readonly TimezoneNameConverter Singleton = new TimezoneNameConverter();
	//}

	//internal class TimezoneOffsetConverter : JsonConverter
	//{
	//	public override bool CanConvert(Type t) => t == typeof(TimezoneOffset) || t == typeof(TimezoneOffset?);

	//	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	//	{
	//		if (reader.TokenType == JsonToken.Null) return null;
	//		var value = serializer.Deserialize<string>(reader);
	//		switch (value)
	//		{
	//			case "+02:00":
	//				return TimezoneOffset.The0200;
	//			case "+03:00":
	//				return TimezoneOffset.The0300;
	//		}
	//		throw new Exception("Cannot unmarshal type TimezoneOffset");
	//	}

	//	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	//	{
	//		if (untypedValue == null)
	//		{
	//			serializer.Serialize(writer, null);
	//			return;
	//		}
	//		var value = (TimezoneOffset)untypedValue;
	//		switch (value)
	//		{
	//			case TimezoneOffset.The0200:
	//				serializer.Serialize(writer, "+02:00");
	//				return;
	//			case TimezoneOffset.The0300:
	//				serializer.Serialize(writer, "+03:00");
	//				return;
	//		}
	//		throw new Exception("Cannot marshal type TimezoneOffset");
	//	}

	//	public static readonly TimezoneOffsetConverter Singleton = new TimezoneOffsetConverter();
	//}
}
