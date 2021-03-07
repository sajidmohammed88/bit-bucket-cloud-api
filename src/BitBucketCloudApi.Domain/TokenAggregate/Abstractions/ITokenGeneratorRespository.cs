using System.Threading.Tasks;

namespace BitBucketCloudApi.Domain.TokenAggregate.Abstractions
{
    public interface ITokenGeneratorRespository
    {
        Task<AccessToken> GenerateToken();
    }
}
