using System;
using Microsoft.Extensions.Options;

namespace BitBucketCloudApi.Infrastructure.Configurations
{
    public class BitBucketCloudApiRepositoryConfiguration : IOptions<BitBucketCloudApiRepositoryConfiguration>
    {
        public const string RepositoryConfiguration = "RepositoryConfiguration";

        public Uri BaseUrl { get; set; }

        BitBucketCloudApiRepositoryConfiguration IOptions<BitBucketCloudApiRepositoryConfiguration>.Value => this;
    }
}
