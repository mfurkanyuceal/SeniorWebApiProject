using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeniorWepApiProject.Options;
using SeniorWepApiProject.Services;

namespace SeniorWepApiProject.Installers
{
    public class FacebookAuthInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var facebookAuthSettings = new FacebookAuthSettings();
            configuration.Bind(nameof(FacebookAuthSettings), facebookAuthSettings);
            services.AddSingleton(facebookAuthSettings);

            services.AddHttpClient();
            services.AddSingleton<IFacebookAuthService, FacebookAuthService>();
        }
    }
}