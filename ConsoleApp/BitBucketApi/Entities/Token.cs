using Newtonsoft.Json;

namespace BitBucketCloudApiConsole.BitBucketApi.Entities
{
    /// <summary>
    /// The token class.
    /// </summary>
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
