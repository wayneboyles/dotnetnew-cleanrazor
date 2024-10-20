using CleanRazor.Security;
using CleanRazor.Web.Services;

namespace CleanRazor.Web
{
    public static class CleanRazorWebExtensions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddTransient<ICurrentUser, CurrentUser>();

            // Automapper
            services.AddAutoMapper(options =>
            {
                options.AddProfile<CleanRazorApplicationMapperProfile>();
            });

            return services;
        }
    }
}
