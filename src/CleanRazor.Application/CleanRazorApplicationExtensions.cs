using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CleanRazor
{
    public static class CleanRazorApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Add application services to the collection
            
            // Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Return
            return services;
        }

    }
}
