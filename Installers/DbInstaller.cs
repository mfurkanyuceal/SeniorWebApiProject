using SeniorWepApiProject.Data;
using SeniorWepApiProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeniorWepApiProject.Domain.IdentityModels;

namespace SeniorWepApiProject.Installers
{
    public class DbInstaller : IInstaller
    {
        void IInstaller.InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<AppUser,AppRole>(option => {
                option.User.RequireUniqueEmail=true;
            }).AddEntityFrameworkStores<DataContext>();
            
            services.AddControllersWithViews();
           services.AddRazorPages();

           //services.AddScoped<IPostService,PostService>(); 


        }
    }
}