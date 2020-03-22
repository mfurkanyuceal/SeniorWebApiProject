using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeniorWepApiProject.Data;
using SeniorWepApiProject.Domain.AppUserModels;

namespace SeniorWepApiProject.Installers
{
    public class DbInstaller : IInstaller
    {
        void IInstaller.InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, AppRole>(option => { option.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<DataContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }
    }
}