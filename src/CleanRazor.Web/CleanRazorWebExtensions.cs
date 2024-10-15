namespace CleanRazor.Web
{
    public static class CleanRazorWebExtensions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Automapper
            services.AddAutoMapper(options =>
            {
                options.AddProfile<CleanRazorApplicationMapperProfile>();
            });

            return services;
        }
    }
}
