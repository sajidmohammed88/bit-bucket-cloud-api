using BitBucketCloudApiConsole.BitBucketApi.Abstractions;
using BitBucketCloudApiConsole.BitBucketApi.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BitBucketCloudApiConsole
{
    /// <summary>
    /// The main entry class.
    /// </summary>
    public partial class Program
    {
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// The main function.
        /// </summary>
        /// <param name="args">The args.</param>
        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build();

            try
            {
                IBitBucketCloudApiClient bitBucketCloudApiClient = _serviceProvider.GetRequiredService<IBitBucketCloudApiClient>();

                Token token = await bitBucketCloudApiClient.GenerateToken(null);
                Console.WriteLine($"Token : {token.AccessToken}");

                IList<string> repos = await bitBucketCloudApiClient.GetRepositories(token.AccessToken);
                Console.WriteLine(string.Join(", ", repos));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
