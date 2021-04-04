using Microsoft.Extensions.Options;
using System;

namespace BitBucketCloudApi.Infrastructure.Configurations
{
    public class TokenGeneratorConfiguration : IOptions<TokenGeneratorConfiguration>
    {
        public const string TokenConfiguration = "TokenConfiguration";

        public Uri BaseUrl { get; set; }

        public string Key { get; set; }

        public string Secret { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        TokenGeneratorConfiguration IOptions<TokenGeneratorConfiguration>.Value => this;
    }
}
