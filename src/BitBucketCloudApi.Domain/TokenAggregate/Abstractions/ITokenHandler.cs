using System.Threading.Tasks;

namespace BitBucketCloudApi.Domain.TokenAggregate.Abstractions
{
    public interface ITokenHandler
    {
        Task<AccessToken> GenerateToken();
    }
}
