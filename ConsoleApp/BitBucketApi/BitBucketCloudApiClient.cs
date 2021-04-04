using BitBucketCloudApiConsole.BitBucketApi.Abstractions;
using BitBucketCloudApiConsole.BitBucketApi.Configuration;
using BitBucketCloudApiConsole.BitBucketApi.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BitBucketCloudApiConsole.BitBucketApi
{
    /// <summary>
    /// The bit bucket cloud api client.
    /// </summary>
    public class BitBucketCloudApiClient : IBitBucketCloudApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly BitBucketCloudApiConfiguration _bucketCloudApiConfiguration;

        public BitBucketCloudApiClient(IHttpClientFactory httpClientFactory, IOptions<BitBucketCloudApiConfiguration> options)
        {
            _httpClientFactory = httpClientFactory;
            _bucketCloudApiConfiguration = options.Value;
        }

        /// <inheritdoc />
        public async Task<Token> GenerateToken(string refreshToken)
        {
            string content = refreshToken != null
                ? $"grant_type=refresh_token&refresh_token={refreshToken}"
                : $"grant_type=password&username={_bucketCloudApiConfiguration.UserName}&password={_bucketCloudApiConfiguration.Password}";

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{_bucketCloudApiConfiguration.BaseUrl}")
            {
                Content = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            request
                .Headers
                .TryAddWithoutValidation("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_bucketCloudApiConfiguration.Key}:{_bucketCloudApiConfiguration.Secret}"))}");

            using HttpClient httpClient = PrepareHttpClientHeader();
            var response = await httpClient.SendAsync(request);
            return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync()) : null;
        }

        /// <inheritdoc />
        public async Task<IList<string>> GetRepositories(string token)
        {
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{_bucketCloudApiConfiguration.RepositoryBaseUrl}?role=admin");
            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");

            using HttpClient httpClient = PrepareHttpClientHeader();
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            JObject obj = JObject.Parse(await response.Content.ReadAsStringAsync().ConfigureAwait(false));

            return obj.SelectToken("$.values")
                ?.Select(o => o.SelectToken("slug")?.ToString())
                .Where(r => r != null)
                .ToList();
        }

        /// <summary>
        /// Prepares the HTTP client header.
        /// </summary>
        /// <returns>The http client</returns>
        private HttpClient PrepareHttpClientHeader()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = false;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
