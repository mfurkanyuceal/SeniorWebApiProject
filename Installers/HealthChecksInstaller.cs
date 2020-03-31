using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeniorWepApiProject.Contracts.HealthChecks;
using SeniorWepApiProject.Data;

namespace SeniorWepApiProject.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<DataContext>()
                .AddCheck<RedisHealthCheck>("Redis");
        }
    }
}