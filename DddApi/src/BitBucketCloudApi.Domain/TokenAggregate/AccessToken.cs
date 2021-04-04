using System.Text.Json.Serialization;

namespace BitBucketCloudApi.Domain.TokenAggregate
{
    public class AccessToken
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
        
        [JsonPropertyName("expires_in")]
        public int ExpirationTime { get; set; }
    }
}
