using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebUI.Filters;
using WebUI.Services;

namespace WebUI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

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
