using System.Linq.Expressions;
using System.Reflection;

using CleanRazor.Data;
using CleanRazor.Entities;

using Microsoft.EntityFrameworkCore;

namespace CleanRazor.EntityFrameworkCore
{
    public sealed class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// <para>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run. However, it will still run when creating a compiled model.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
        /// examples.
        /// </para>
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            var auditedType = typeof(IAudited);
            var softDeleteType = typeof(ISoftDelete);

            // Configure
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Soft Delete
                if (softDeleteType.IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType).Property<string>("DeletedBy").IsRequired(false).HasMaxLength(128);
                    modelBuilder.Entity(entity.ClrType).HasQueryFilter(GenerateQueryFilterExpression(entity.ClrType));
                }
                else if(auditedType.IsAssignableFrom(entity.ClrType))
                {
                    // Audited Entity
                    modelBuilder.Entity(entity.ClrType).Property<string>("CreatedBy").IsRequired(false).HasMaxLength(128);
                    modelBuilder.Entity(entity.ClrType).Property<string>("ModifiedBy").IsRequired(false).HasMaxLength(128);
                }
            }
        }

        #region Soft Delete Methods

        private LambdaExpression GenerateQueryFilterExpression(Type type)
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
