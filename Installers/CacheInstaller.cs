using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeniorWepApiProject.Cache;
using SeniorWepApiProject.Services;
using StackExchange.Redis;

namespace SeniorWepApiProject.Installers
{
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSetttings = new RedisCacheSettings();

            configuration.GetSection(nameof(RedisCacheSettings)).Bind(redisCacheSetttings);

            services.AddSingleton(redisCacheSetttings);

            if (!redisCacheSetttings.Enabled)
            {
                return;
            }

            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(redisCacheSetttings.ConnectionString));
            services.AddStackExchangeRedisCache(options =>
                options.Configuration = redisCacheSetttings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}