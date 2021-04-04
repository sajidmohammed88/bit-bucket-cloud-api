using BitBucketCloudApi.Domain.TokenAggregate;
using BitBucketCloudApi.Domain.TokenAggregate.Abstractions;
using BitBucketCloudApi.Infrastructure.Configurations;
using BitBucketCloudApi.Infrastructure.Serializer;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace BitBucketCloudApi.Infrastructure.Repositories
{
    /// <summary>
    /// The token generator repository class.
    /// </summary>
    /// <seealso cref="ITokenGeneratorRepository" />
    public class TokenGeneratorRepository : ITokenGeneratorRepository
    {
        private readonly TokenGeneratorConfiguration _tokenGeneratorConfiguration;

        private IFlurlRequest BaseRequest => _tokenGeneratorConfiguration
            .BaseUrl
            .AbsoluteUri
            .ConfigureRequest(setting => setting.JsonSerializer = new FlurlSerializer());

        public TokenGeneratorRepository(IOptions<TokenGeneratorConfiguration> options)
        {
            _tokenGeneratorConfiguration = options.Value ?? throw new ArgumentNullException(nameof(TokenGeneratorConfiguration));
        }

        /// <inheritdoc />
        public async Task<AccessToken> GenerateToken()
        {
            IFlurlRequest request = BaseRequest;
            request
                .Headers
                .AddOrReplace("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_tokenGeneratorConfiguration.Key}:{_tokenGeneratorConfiguration.Secret}"))}");

            return await request
                .PostUrlEncodedAsync($"grant_type=password&username={_tokenGeneratorConfiguration.UserName}&password={_tokenGeneratorConfiguration.Password}")
                .ReceiveJson<AccessToken>()
                .ConfigureAwait(false);
        }
    }
}
