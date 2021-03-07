using BitBucketCloudApi.Domain.TokenAggregate;
using BitBucketCloudApi.Domain.TokenAggregate.Abstractions;
using BitBucketCloudApi.Infrastructure.Configurations;
using BitBucketCloudApi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace BitBucketCloudApi.Api.Bootstrap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("Default", () => HealthCheckResult.Healthy("OK"));

            services.Configure<TokenGeneratorConfiguration>(Configuration.GetSection(TokenGeneratorConfiguration.TokenConfiguration));

            services.AddScoped<ITokenGeneratorRespository, TokenGeneratorRespository>();
            services.AddScoped<ITokenHandler, TokenHandler>();

            services.AddMemoryCache();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
