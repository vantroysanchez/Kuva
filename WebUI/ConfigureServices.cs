using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebUI.Filters;
using WebUI.Services;

namespace WebUI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opc => opc.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["KeyJwt"])),
                ClockSkew = TimeSpan.Zero
            });

            services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());
            //.AddFluentValidation(x => x.AutomaticValidationEnabled = false); --Deprecated https://github.com/FluentValidation/FluentValidation/issues/1965
            services.AddFluentValidationClientsideAdapters();            

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            return services;
        }
    }
}
