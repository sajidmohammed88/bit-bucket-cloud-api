using BitBucketCloudApi.Domain.TokenAggregate.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace BitBucketCloudApi.Domain.TokenAggregate
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IMemoryCache _cache;
        private readonly ITokenGeneratorRespository _tokenGeneratorRespository;

        private const string _cacheKey = "_AccessToken";

        public TokenHandler(IMemoryCache cache, ITokenGeneratorRespository tokenGeneratorRespository)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _tokenGeneratorRespository = tokenGeneratorRespository ?? throw new ArgumentNullException(nameof(tokenGeneratorRespository));
        }

        public async Task<AccessToken> GenerateToken()
        {
            return await _cache.GetOrCreateAsync<AccessToken>(_cacheKey, async e =>
            {
                AccessToken accessToken = await _tokenGeneratorRespository.GenerateToken().ConfigureAwait(false);
                e.SlidingExpiration = TimeSpan.FromSeconds(accessToken.ExpirationTime);

                return accessToken;
            });
        }
    }
}
