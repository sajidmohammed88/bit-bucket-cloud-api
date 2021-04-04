using BitBucketCloudApiConsole.BitBucketApi;
using BitBucketCloudApiConsole.BitBucketApi.Abstractions;
using BitBucketCloudApiConsole.BitBucketApi.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace BitBucketCloudApiConsole
{
    /// <summary>
    /// The configuration of project.
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private static IConfiguration _configuration;

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Host builder.</returns>
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(AddConfiguration)
                .ConfigureServices(ConfigureServices);

        /// <summary>
        /// Add configuration.
        /// </summary>
        /// <param name="builder">The configuration builder.</param>
        /// <returns>The configuration builder.</returns>
        private static void AddConfiguration(IConfigurationBuilder builder)
        {
            IConfigurationBuilder configurationBuilder =
                builder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = configurationBuilder.Build();
        }

        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services">The services</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpClient();

            services
                .Configure<BitBucketCloudApiConfiguration>(_configuration.GetSection("BitBucketConfiguration"));

            services.AddScoped<IBitBucketCloudApiClient, BitBucketCloudApiClient>();

            _serviceProvider = services.BuildServiceProvider();
        }
    }
}
