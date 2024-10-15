using CleanRazor.EntityFrameworkCore.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanRazor.EntityFrameworkCore
{
    public static class EntityFrameworkCoreExtensions
    {
        public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            // Get the connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found");

            // Interceptors
            services.AddScoped<ISaveChangesInterceptor, SoftDeleteInterceptor>();
            
            // Add the DB Context
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
#if (UseSqlServer)
                // SQL Server
                options.UseSqlServer(connectionString);
#endif
#if (UseLocalDB)
                // LocalDB
                options.UseSqlite(connectionString);
#endif
#if (UsePostgreSQL)
                // PostgreSQL
                options.UseNpgsql(connectionString);
#endif
                // Interceptors
                options.AddInterceptors(provider.GetRequiredService<ISaveChangesInterceptor>());
            });

            return services;
        }
    }
}
