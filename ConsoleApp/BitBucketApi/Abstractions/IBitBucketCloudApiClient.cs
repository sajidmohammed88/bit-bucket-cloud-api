using BitBucketCloudApiConsole.BitBucketApi.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitBucketCloudApiConsole.BitBucketApi.Abstractions
{
    /// <summary>
    /// The bit bucket cloud api interface.
    /// </summary>
    public interface IBitBucketCloudApiClient
    {
        /// <summary>
        /// Generate the token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>The generated token.</returns>
        Task<Token> GenerateToken(string refreshToken);

        /// <summary>
        /// Gets the repositories.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The repositories.</returns>
        Task<IList<string>> GetRepositories(string token);
    }
}
