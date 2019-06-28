using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LeyboldCrawler.Model.Targoman.Response
{
    public partial class TargomanResponse
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("tuid")]
        public string Tuid { get; set; }

        [JsonProperty("TrTime")]
        public double TrTime { get; set; }

        [JsonProperty("serverID")]
        public string ServerId { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("errno")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Errno { get; set; }

        [JsonProperty("tr")]
        public Tr Tr { get; set; }

        [JsonProperty("rpcTime")]
        public double RpcTime { get; set; }
    }

    public partial class Tr
    {
        [JsonProperty("base")]
        public string[][] Base { get; set; }

        [JsonProperty("phrases")]
        public Phrase[][][] Phrases { get; set; }

        [JsonProperty("alignments")]
        public TrAlignment[][][] Alignments { get; set; }
    }

    /// <summary>
    /// Basic Auth
    ///
    /// GET https://postman-echo.com/basic-auth
    ///
    /// This endpoint simulates a **basic-auth** protected endpoint.
    /// The endpoint accepts a default username and password and returns a status code of `200
    /// ok` only if the same is provided.
    /// Otherwise it will return a status code `401 unauthorized`.
    ///
    /// > Username: `postman`
    /// >
    /// > Password: `password`
    ///
    /// To use this endpoint, send a request with the header `Authorization: Basic
    /// cG9zdG1hbjpwYXNzd29yZA==`.
    /// The cryptic latter half of the header value is a base64 encoded concatenation of the
    /// default username and password.
    /// Using Postman, to send this request, you can simply fill in the username and password in
    /// the "Authorization" tab and Postman will do the rest for you.
    ///
    /// To know more about basic authentication, refer to the [Basic Access
    /// Authentication](https://en.wikipedia.org/wiki/Basic_access_authentication) wikipedia
    /// article.
    /// The article on [authentication
    ///
    /// helpers](https://www.getpostman.com/docs/helpers#basic-auth?source=echo-collection-app-onboarding)
    /// elaborates how to use the same within the Postman app.
    /// </summary>
    public partial class BasicAuth
    {
        [JsonProperty("authenticated")]
        public bool Authenticated { get; set; }
    }

    /// <summary>
    /// OAuth1.0 Verify Signature
    ///
    /// GET https://postman-echo.com/oauth1
    ///
    /// OAuth1.0a is a specification that defines a protocol that can be used by one
    /// service to access "protected" resources (endpoints) on another service. A
    /// major part of OAuth1.0 is HTTP Request Signing. This endpoint allows you to
    /// check whether the request calculation works properly in the client.
    ///
    /// The endpoint supports the HTTP ``Authorization`` header. In case the signature
    /// verification fails, the endpoint provides the four debug values,
    ///
    /// * ``base_uri``
    /// * ``normalized_param_string``
    /// * ``base_string``
    /// * ``signing_key``
    ///
    /// For more details about these parameters, check the [OAuth1.0a
    /// Specification](http://oauth.net/core/1.0a/)
    ///
    /// In order to use this endpoint, you can set the following values:
    ///
    /// > Consumer Key: ``RKCGzna7bv9YD57c``
    /// >
    /// > Consumer Secret: ``D+EdQ-gs$-%@2Nu7``
    ///
    /// If you are using Postman, also check the "Add params to header" and
    /// "Auto add parameters" boxes.
    /// </summary>
    public partial class OAuth10VerifySignature
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("base_uri", NullValueHandling = NullValueHandling.Ignore)]
        public Uri BaseUri { get; set; }

        [JsonProperty("normalized_param_string", NullValueHandling = NullValueHandling.Ignore)]
        public string NormalizedParamString { get; set; }

        [JsonProperty("base_string", NullValueHandling = NullValueHandling.Ignore)]
        public string BaseString { get; set; }

        [JsonProperty("signing_key", NullValueHandling = NullValueHandling.Ignore)]
        public string SigningKey { get; set; }
    }

    /// <summary>
    /// Hawk Auth
    ///
    /// GET https://postman-echo.com/auth/hawk
    ///
    /// This endpoint is a Hawk Authentication protected endpoint. [Hawk
    /// authentication](https://github.com/hueniverse/hawk) is a widely used protocol for
    /// protecting API endpoints. One of Hawk's main goals is to enable HTTP authentication for
    /// services that do not use TLS (although it can be used in conjunction with TLS as well).
    ///
    /// In order to use this endpoint, select the "Hawk Auth" helper inside Postman, and set the
    /// following values:
    ///
    /// Hawk Auth ID: `dh37fgj492je`
    ///
    /// Hawk Auth Key: `werxhqb98rpaxn39848xrunpaw3489ruxnpa98w4rxn`
    ///
    /// Algorithm: `sha256`
    ///
    /// The rest of the values are optional, and can be left blank. Hitting send should give you
    /// a response with a status code of 200 OK.
    /// </summary>
    public partial class HawkAuth
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    /// <summary>
    /// Set Cookies
    ///
    /// GET https://postman-echo.com/cookies/set?foo1=bar1&foo2=bar2
    ///
    /// The cookie setter endpoint accepts a list of cookies and their values as part of URL
    /// parameters of a `GET` request. These cookies are saved and can be subsequently retrieved
    /// or deleted. The response of this request returns a JSON with all cookies listed.
    ///
    /// To set your own set of cookies, simply replace the URL parameters "foo1=bar1&foo2=bar2"
    /// with your own set of key-value pairs.
    /// </summary>
    public partial class SetCookies
    {
        [JsonProperty("cookies")]
        public SetCookiesCookies Cookies { get; set; }
    }

    public partial class SetCookiesCookies
    {
        [JsonProperty("foo1")]
        public string Foo1 { get; set; }

        [JsonProperty("foo2")]
        public string Foo2 { get; set; }
    }

    /// <summary>
    /// Get Cookies
    ///
    /// GET https://postman-echo.com/cookies
    ///
    /// Use this endpoint to get a list of all cookies that are stored with respect to this
    /// domain. Whatever key-value pairs that has been previously set by calling the "Set
    /// Cookies" endpoint, will be returned as response JSON.
    /// </summary>
    public partial class GetCookies
    {
        [JsonProperty("cookies")]
        public GetCookiesCookies Cookies { get; set; }
    }

    public partial class GetCookiesCookies
    {
        [JsonProperty("foo2")]
        public string Foo2 { get; set; }
    }

    /// <summary>
    /// Delete Cookies
    ///
    /// GET https://postman-echo.com/cookies/delete?foo1&foo2
    ///
    /// One or more cookies that has been set for this domain can be deleted by providing the
    /// cookie names as part of the URL parameter. The response of this request is a JSON
    /// containing the list of currently set cookies.
    /// </summary>
    public partial class DeleteCookies
    {
        [JsonProperty("cookies")]
        public GetCookiesCookies Cookies { get; set; }
    }

    /// <summary>
    /// Request Headers
    ///
    /// GET https://postman-echo.com/headers
    ///
    /// A `GET` request to this endpoint returns the list of all request headers as part of the
    /// response JSON.
    /// In Postman, sending your own set of headers through the [Headers
    ///
    /// tab](https://www.getpostman.com/docs/requests#headers?source=echo-collection-app-onboarding)
    /// will reveal the headers as part of the response.
    /// </summary>
    public partial class RequestHeaders
    {
        [JsonProperty("headers")]
        public Headers Headers { get; set; }
    }

    public partial class Headers
    {
        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("accept")]
        public string Accept { get; set; }

        [JsonProperty("accept-encoding")]
        public string AcceptEncoding { get; set; }

        [JsonProperty("accept-language")]
        public string AcceptLanguage { get; set; }

        [JsonProperty("cache-control")]
        public string CacheControl { get; set; }

        [JsonProperty("my-sample-header")]
        public string MySampleHeader { get; set; }

        [JsonProperty("postman-token")]
        public Guid PostmanToken { get; set; }

        [JsonProperty("user-agent")]
        public string UserAgent { get; set; }

        [JsonProperty("x-forwarded-port")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long XForwardedPort { get; set; }

        [JsonProperty("x-forwarded-proto")]
        public string XForwardedProto { get; set; }
    }

    /// <summary>
    /// Response Headers
    ///
    /// GET
    /// https://postman-echo.com/response-headers?Content-Type=text/html&test=response_headers
    ///
    /// This endpoint causes the server to send custom set of response headers. Providing header
    /// values as part of the URL parameters of a `GET` request to this endpoint returns the same
    /// as part of response header.
    ///
    /// To send your own set of headers, simply add or replace the the URL parameters with your
    /// own set.
    /// </summary>
    public partial class ResponseHeaders
    {
        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }

        [JsonProperty("test")]
        public string Test { get; set; }
    }

    /// <summary>
    /// Response Status Code
    ///
    /// GET https://postman-echo.com/status/200
    ///
    /// This endpoint allows one to instruct the server which status code to respond with.
    ///
    /// Every response is accompanied by a status code. The status code provides a summary of the
    /// nature of response sent by the server. For example, a status code of `200` means
    /// everything is okay with the response and a code of `404` implies that the requested URL
    /// does not exist on server.
    /// A list of all valid HTTP status code can be found at the [List of Status
    /// Codes](https://en.wikipedia.org/wiki/List_of_HTTP_status_codes) wikipedia article. When
    /// using Postman, the response status code is described for easy reference.
    ///
    /// Note that if an invalid status code is requested to be sent, the server returns a status
    /// code of `400 Bad Request`.
    /// </summary>
    public partial class ResponseStatusCode
    {
        [JsonProperty("status")]
        public long Status { get; set; }
    }

    /// <summary>
    /// Delay Response
    ///
    /// GET https://postman-echo.com/delay/3
    ///
    /// Using this endpoint one can configure how long it takes for the server to come back with
    /// a response. Appending a number to the URL defines the time (in seconds) the server will
    /// wait before responding.
    ///
    /// Note that a maximum delay of 10 seconds is accepted by the server.
    /// </summary>
    public partial class DelayResponse
    {
        [JsonProperty("delay")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Delay { get; set; }
    }

    /// <summary>
    /// Timestamp validity
    ///
    /// GET https://postman-echo.com/time/valid?timestamp=2016-10-10
    ///
    /// A simple `GET` request to `/time/valid` to determine the validity of the timestamp,
    /// (current by default).
    /// This endpoint accepts `timestamp`, `locale`, `format`, and `strict` query parameters to
    /// construct the date time instance to check against.
    ///
    /// Responses are provided in JSON format, with a valid key to indicate the result. The
    /// response code is `200`.
    ///
    /// ```
    /// {
    /// valid: true/false
    /// }
    /// ```
    /// </summary>
    public partial class TimestampValidity
    {
        [JsonProperty("valid")]
        public bool Valid { get; set; }
    }

    /// <summary>
    /// Transform collection from format v1 to v2
    ///
    /// POST https://postman-echo.com/transform/collection?from=1&to=2
    /// </summary>
    public partial class TransformCollectionFromFormatV1ToV2
    {
        [JsonProperty("variables")]
        public object[] Variables { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("item")]
        public Item[] Item { get; set; }
    }

    public partial class Info
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("_postman_id")]
        public Guid PostmanId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("schema")]
        public Uri Schema { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("event", NullValueHandling = NullValueHandling.Ignore)]
        public Event[] Event { get; set; }

        [JsonProperty("request")]
        public ItemRequest Request { get; set; }

        [JsonProperty("response")]
        public object[] Response { get; set; }
    }

    public partial class Event
    {
        [JsonProperty("listen")]
        public string Listen { get; set; }

        [JsonProperty("script")]
        public Script Script { get; set; }
    }

    public partial class Script
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("exec")]
        public string[] Exec { get; set; }
    }

    public partial class ItemRequest
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("header")]
        public Header[] Header { get; set; }

        [JsonProperty("body")]
        public Body Body { get; set; }
    }

    public partial class Body
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }
    }

    public partial class Header
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    /// <summary>
    /// Transform collection from format v2 to v1
    ///
    /// POST https://postman-echo.com/transform/collection?from=2&to=1
    /// </summary>
    public partial class TransformCollectionFromFormatV2ToV1
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order")]
        public Guid[] Order { get; set; }

        [JsonProperty("folders")]
        public object[] Folders { get; set; }

        [JsonProperty("requests")]
        public RequestElement[] Requests { get; set; }
    }

    public partial class RequestElement
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("collectionId")]
        public Guid CollectionId { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("headers")]
        public string Headers { get; set; }

        [JsonProperty("data")]
        public object[] Data { get; set; }

        [JsonProperty("rawModeData")]
        public string RawModeData { get; set; }

        [JsonProperty("tests", NullValueHandling = NullValueHandling.Ignore)]
        public string Tests { get; set; }

        [JsonProperty("preRequestScript", NullValueHandling = NullValueHandling.Ignore)]
        public string PreRequestScript { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("dataMode", NullValueHandling = NullValueHandling.Ignore)]
        public string DataMode { get; set; }
    }

    public partial struct AlignmentAlignment
    {
        public bool? Bool;
        public string String;

        public static implicit operator AlignmentAlignment(bool Bool) => new AlignmentAlignment { Bool = Bool };
        public static implicit operator AlignmentAlignment(string String) => new AlignmentAlignment { String = String };
    }

    public partial struct TrAlignment
    {
        public AlignmentAlignment[][] AnythingArrayArray;
        public long? Integer;
        public string String;

        public static implicit operator TrAlignment(AlignmentAlignment[][] AnythingArrayArray) => new TrAlignment { AnythingArrayArray = AnythingArrayArray };
        public static implicit operator TrAlignment(long Integer) => new TrAlignment { Integer = Integer };
        public static implicit operator TrAlignment(string String) => new TrAlignment { String = String };
    }

    public partial struct Phrase
    {
        public long? Integer;
        public string String;

        public static implicit operator Phrase(long Integer) => new Phrase { Integer = Integer };
        public static implicit operator Phrase(string String) => new Phrase { String = String };
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TrAlignmentConverter.Singleton,
                AlignmentAlignmentConverter.Singleton,
                PhraseConverter.Singleton,
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

    internal class TrAlignmentConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TrAlignment) || t == typeof(TrAlignment?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new TrAlignment { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new TrAlignment { String = stringValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<AlignmentAlignment[][]>(reader);
                    return new TrAlignment { AnythingArrayArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type TrAlignment");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (TrAlignment)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.AnythingArrayArray != null)
            {
                serializer.Serialize(writer, value.AnythingArrayArray);
                return;
            }
            throw new Exception("Cannot marshal type TrAlignment");
        }

        public static readonly TrAlignmentConverter Singleton = new TrAlignmentConverter();
    }

    internal class AlignmentAlignmentConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(AlignmentAlignment) || t == typeof(AlignmentAlignment?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new AlignmentAlignment { Bool = boolValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new AlignmentAlignment { String = stringValue };
            }
            throw new Exception("Cannot unmarshal type AlignmentAlignment");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (AlignmentAlignment)untypedValue;
            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            throw new Exception("Cannot marshal type AlignmentAlignment");
        }

        public static readonly AlignmentAlignmentConverter Singleton = new AlignmentAlignmentConverter();
    }

    internal class PhraseConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Phrase) || t == typeof(Phrase?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new Phrase { Integer = integerValue };
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new Phrase { String = stringValue };
            }
            throw new Exception("Cannot unmarshal type Phrase");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Phrase)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            throw new Exception("Cannot marshal type Phrase");
        }

        public static readonly PhraseConverter Singleton = new PhraseConverter();
    }
}
