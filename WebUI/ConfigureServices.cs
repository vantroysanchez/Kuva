using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebUI.Attributes;
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

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddAuthentication( x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(c =>
            {
                c.Cookie.Name = "token";
            })
            .AddJwtBearer(opc => {
                opc.RequireHttpsMetadata = false;
                opc.SaveToken = true;
                opc.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["KeyJwt"])),
                    ClockSkew = TimeSpan.Zero

                };
                opc.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());
            //.AddFluentValidation(x => x.AutomaticValidationEnabled = false); --Deprecated https://github.com/FluentValidation/FluentValidation/issues/1965
            services.AddFluentValidationClientsideAdapters();            

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("AdminAccess", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.Requirements.Add(new AdminAccessAttribute());
                });                              
            });

            services.AddSingleton<IAuthorizationHandler, AdminAccessHandler>();

            return services;
        }
    }
}
