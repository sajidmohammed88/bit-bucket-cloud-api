using BitBucketCloudApi.Domain.BitBucketCloudAggregate.Abstractions;
using BitBucketCloudApi.Infrastructure.Configurations;
using BitBucketCloudApi.Infrastructure.Serializer;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BitBucketCloudApi.Infrastructure.Repositories
{
    /// <summary>
    /// The bit bucket cloud api repository class.
    /// </summary>
    /// <seealso cref="IBitBucketCloudApiRepository" />
    public class BitBucketCloudApiRepository : IBitBucketCloudApiRepository
    {
        private readonly BitBucketCloudApiRepositoryConfiguration _repositoryConfiguration;

        private IFlurlRequest BaseRequest => _repositoryConfiguration
            .BaseUrl
            .AbsoluteUri
            .ConfigureRequest(setting => setting.JsonSerializer = new FlurlSerializer());

        public BitBucketCloudApiRepository(IOptions<BitBucketCloudApiRepositoryConfiguration> options)
        {
            _repositoryConfiguration = options.Value ?? throw new ArgumentNullException(nameof(BitBucketCloudApiRepositoryConfiguration));
        }

        /// <inheritdoc />
        public async Task<IList<string>> GetRepositories(string token)
        {
            IFlurlResponse response = await BaseRequest
                .SetQueryParam("role", "admin")
                .WithOAuthBearerToken(token)
                .AllowHttpStatus(HttpStatusCode.OK)
                .GetAsync();

            JObject obj = JObject.Parse(await response.GetStringAsync().ConfigureAwait(false));
            response.Dispose();

            return obj.SelectToken("$.values").Select(o => o.SelectToken("slug").ToString()).ToList();
        }
    }
}
