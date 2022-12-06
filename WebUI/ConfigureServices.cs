using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebUI.Services;

namespace WebUI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            return services;
        }
    }
}
