using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitBucketCloudApi.Domain.BitBucketCloudAggregate.Abstractions
{
    /// <summary>
    /// The bit bucket cloud api repository interface.
    /// </summary>
    public interface IBitBucketCloudApiRepository
    {
        /// <summary>
        /// Gets the repositories.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The repositories.</returns>
        Task<IList<string>> GetRepositories(string token);
    }
}
