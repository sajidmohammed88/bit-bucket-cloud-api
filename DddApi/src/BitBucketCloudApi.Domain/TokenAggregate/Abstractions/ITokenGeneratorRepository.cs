using System.Threading.Tasks;

namespace BitBucketCloudApi.Domain.TokenAggregate.Abstractions
{
    /// <summary>
    /// The token generator repository interface.
    /// </summary>
    public interface ITokenGeneratorRepository
    {
        /// <summary>
        /// Generate the token.
        /// </summary>
        /// <returns>Generated token.</returns>
        Task<AccessToken> GenerateToken();
    }
}
