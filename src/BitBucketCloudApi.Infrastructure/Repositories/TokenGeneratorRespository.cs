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
    public class TokenGeneratorRespository : ITokenGeneratorRespository
    {
        private readonly TokenGeneratorConfiguration _tokenGeneratorConfiguration;

        private IFlurlRequest BaseRequest => _tokenGeneratorConfiguration.BaseUrl.AbsoluteUri.ConfigureRequest(setting => setting.JsonSerializer = new FlurlSerializer());

        public TokenGeneratorRespository(IOptions<TokenGeneratorConfiguration> options)
        {
            _tokenGeneratorConfiguration = options.Value;
        }

        public async Task<AccessToken> GenerateToken()
        {
            IFlurlRequest flurlRequest = BaseRequest;
            flurlRequest
                .Headers
                .AddOrReplace("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_tokenGeneratorConfiguration.Key}:{_tokenGeneratorConfiguration.Secret}"))}");

            return await flurlRequest
                .PostUrlEncodedAsync($"grant_type=password&username={_tokenGeneratorConfiguration.UserName}&password={_tokenGeneratorConfiguration.Password}")
                .ReceiveJson<AccessToken>()
                .ConfigureAwait(false);
        }
    }
}
