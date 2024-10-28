using System.Linq.Expressions;
using CleanRazor.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanRazor.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Repository class to inherit from
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="CleanRazor.Data.IRepository&lt;T&gt;" />
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="System.IAsyncDisposable" />
    public abstract class Repository<T>(AppDbContext context) : IRepository<T>, IDisposable, IAsyncDisposable where T : class
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="saveChanges">Whether to save the changes</param>
        public void Add(T entity, bool saveChanges = false)
        {
            context.Set<T>().Add(entity);

            if (saveChanges)
            {
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Adds a range of entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="saveChanges">Whether to save the changes</param>
        public void AddRange(IEnumerable<T> entities, bool saveChanges = false)
        {
            context.Set<T>().AddRange(entities);

            if (saveChanges)
            {
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T? GetById<TKey>(TKey id)
        {
            return context.Set<T>().Find(id);
        }

        /// <summary>
        /// Gets the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the specified entity.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public T? Get(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets the specified entity.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Gets a list of entities.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public IEnumerable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where<T>(predicate).ToList();
        }

        /// <summary>
        /// Gets a list of entities.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().Where<T>(predicate).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Gets all the entities.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        /// <summary>
        /// Gets all the entities.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Counts the number of entities.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return context.Set<T>().Count();
        }

        /// <summary>
        /// Counts the number of entities.
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await context.Set<T>().CountAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="saveChanges">Whether to save the changes</param>
        public void Update(T entity, bool saveChanges = false)
        {
            context.Entry(entity).State = EntityState.Modified;

            if (saveChanges)
            {
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="saveChanges">Whether to save the changes</param>
        public void Remove(T entity, bool saveChanges = false)
        {
            context.Set<T>().Remove(entity);

            if (saveChanges)
            {
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        #region Dispose Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous dispose operation.
        /// </returns>
        public async ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
        }

        #endregion
    }
}
