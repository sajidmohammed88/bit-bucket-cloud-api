using System.Text.Json.Serialization;

namespace BitBucketCloudApi.Domain.TokenAggregate
{
    public class AccessToken
    {
        [JsonPropertyName("access_token")]
        public string AccesToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpirationTime { get; set; }
    }
}
