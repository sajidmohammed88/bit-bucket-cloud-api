using BitBucketCloudApi.Domain.TokenAggregate.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace BitBucketCloudApi.Domain.TokenAggregate
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IMemoryCache _cache;
        private readonly ITokenGeneratorRepository _tokenGeneratorRepository;
        private readonly Task<string> _generateTokenTask;

        public TokenHandler(IMemoryCache cache, ITokenGeneratorRepository tokenGeneratorRepository)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _tokenGeneratorRepository = tokenGeneratorRepository ?? throw new ArgumentNullException(nameof(tokenGeneratorRepository));
            _generateTokenTask = GenerateTokenTask();
        }

        private async Task<string> GenerateTokenTask()
        {
            return await _cache.GetOrCreateAsync("AccessToken_", async e =>
            {
                AccessToken accessToken = await _tokenGeneratorRepository.GenerateToken().ConfigureAwait(false);
                e.SlidingExpiration = TimeSpan.FromSeconds(accessToken.ExpirationTime);

                return accessToken.Token;
            });
        }

        /// <inheritdoc />
        public async Task<string> GetAccessToken()
        {
            return await _generateTokenTask.ConfigureAwait(false);
        }
    }
}
