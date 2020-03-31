using System.Net;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SeniorWepApiProject.Authorization;
using SeniorWepApiProject.Filters;
using SeniorWepApiProject.Options;
using SeniorWepApiProject.Services;

namespace SeniorWepApiProject.Installers
{
    public class MvcInstaller : IInstaller
    {
        void IInstaller.InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("172.31.84.140"));
            });

            var jwtSettings = new JwtSettings();

            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ISwapService, SwapService>();

            services.AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(mvcConfiguration =>
                    mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>());

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;
                });


            services.AddAuthorization(
                /*
                options =>
            {
                //Aşırı Hoş bir şey :) Authorization Klasörü bunun için var 
                
                options.AddPolicy("MustWorkForYuceal",
                    policy => policy.AddRequirements(new WorksForCompanyRequirement("yuceal.com")));
            }
                */
            );

            services.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();


            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });
        }
    }
}