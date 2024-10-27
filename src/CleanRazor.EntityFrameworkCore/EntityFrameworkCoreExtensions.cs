using CleanRazor.Data;
using System.Linq.Expressions;
using CleanRazor.Entities;
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

            // Repositories
            // Add your repositories here
            // services.AddTransient<IMyRepository, MyRepository>();

            // Interceptors
            services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
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

        internal static ModelBuilder ConfigureSoftDelete(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Soft Delete
                if (typeof(ISoftDelete).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .Property<string>("DeletedBy")
                        .IsRequired(false)
                        .HasMaxLength(128);

                    modelBuilder.Entity(entity.ClrType)
                        .HasQueryFilter(GenerateSoftDeleteQueryFilterExpression(entity.ClrType));
                }
            }

            return modelBuilder;
        }

        internal static ModelBuilder ConfigureAuditProperties(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAudited).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .Property<string>("CreatedBy")
                        .IsRequired(false)
                        .HasMaxLength(128);

                    modelBuilder.Entity(entity.ClrType)
                        .Property<string>("ModifiedBy")
                        .IsRequired(false)
                        .HasMaxLength(128);
                }
            }

            return modelBuilder;
        }

        #region Soft Delete Methods

        private static LambdaExpression GenerateSoftDeleteQueryFilterExpression(Type type)
        {
            var parameter = Expression.Parameter(type, "sd");
            var falseConstantValue = Expression.Constant(false);
            var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDelete.IsDeleted));
            var equalExpression = Expression.Equal(propertyAccess, falseConstantValue);
            var lambda = Expression.Lambda(equalExpression, parameter);

            return lambda;
        }

        #endregion
    }
}
