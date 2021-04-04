using Flurl.Http.Configuration;
using System.IO;
using System.Text.Json;

namespace BitBucketCloudApi.Infrastructure.Serializer
{
    public class FlurlSerializer : ISerializer
    {
        public T Deserialize<T>(string s)
        {
            return JsonSerializer.Deserialize<T>(s);
        }

        public T Deserialize<T>(Stream stream)
        {
            return JsonSerializer.DeserializeAsync<T>(stream).Result;
        }

        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
