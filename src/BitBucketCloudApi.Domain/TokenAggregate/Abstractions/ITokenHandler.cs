using System.Threading.Tasks;

namespace BitBucketCloudApi.Domain.TokenAggregate.Abstractions
{
    public interface ITokenHandler
    {
        /// <summary>
        /// Get the access token.
        /// </summary>
        /// <returns>The access token.</returns>
        Task<string> GetAccessToken();
    }
}
