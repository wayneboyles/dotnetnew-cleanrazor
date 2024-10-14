using CleanRazor.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanRazor.EntityFrameworkCore
{
    public static class EntityFrameworkCoreExtensions
    {
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration, Action<DbContextOptionsBuilder>? builderAction = null)
        {
            if (builderAction != null)
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    builderAction?.Invoke(options);
                    options.AddInterceptors(new SoftDeleteInterceptor());
                });
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("Default"));
                    options.AddInterceptors(new SoftDeleteInterceptor());
                }); 
            }

            return services;
        }
    }
}
