using BitBucketCloudApi.Domain.BitBucketCloudAggregate.Abstractions;
using BitBucketCloudApi.Domain.TokenAggregate.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitBucketCloudApi.Api.Features
{
    [Route("bit-bucket-cloud-api")]
    [ApiController]
    public class BitBucketCloudApiController : ControllerBase
    {
        private readonly IBitBucketCloudApiRepository _bitBucketCloudApiRepository;
        private readonly string _accessToken;

        public BitBucketCloudApiController(ITokenHandler tokenHandler, IBitBucketCloudApiRepository bitBucketCloudApiRepository)
        {
            if (tokenHandler == null)
            {
                throw new ArgumentNullException(nameof(tokenHandler));
            }

            _bitBucketCloudApiRepository = bitBucketCloudApiRepository ?? throw new ArgumentNullException(nameof(bitBucketCloudApiRepository));
            _accessToken = Task.Run(async () => await tokenHandler.GetAccessToken().ConfigureAwait(false)).Result;
        }

        [HttpGet]
        public async Task<IList<string>> GetRepositories()
        {
            return await _bitBucketCloudApiRepository.GetRepositories(_accessToken).ConfigureAwait(false);
        }
    }
}
