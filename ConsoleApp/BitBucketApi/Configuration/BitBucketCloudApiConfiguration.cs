using System;
using Microsoft.Extensions.Options;

namespace BitBucketCloudApiConsole.BitBucketApi.Configuration
{
    /// <summary>
    /// The token configuration.
    /// </summary>
    public class BitBucketCloudApiConfiguration : IOptions<BitBucketCloudApiConfiguration>
    {
        /// <summary>
        /// Gets or sets the base URL.
        /// </summary>
        public Uri BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the repository base URL.
        /// </summary>
        public Uri RepositoryBaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <inheritdoc />
        BitBucketCloudApiConfiguration IOptions<BitBucketCloudApiConfiguration>.Value => this;
    }
}
