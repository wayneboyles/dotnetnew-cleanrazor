using System.Linq.Expressions;

namespace CleanRazor.Data
{
    /// <summary>
    /// Generic repository interface that provides common methods to access
    /// and modify data.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="saveChanges">Whether to save the changes</param>
        void Add(T entity, bool saveChanges = false);

        /// <summary>
        /// Adds a range of entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="saveChanges">Whether to save the changes</param>
        void AddRange(IEnumerable<T> entities, bool saveChanges = false);

        /// <summary>
        /// Gets the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T? GetById<TKey>(TKey id) where TKey : class;

        /// <summary>
        /// Gets the entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : class;

        /// <summary>
        /// Gets the specified entity.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        T? Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the specified entity.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of entities.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets a list of entities.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all the entities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets all the entities.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts the number of entities.
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Counts the number of entities.
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="saveChanges">Whether to save the changes</param>
        void Update(T entity, bool saveChanges = false);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="saveChanges">Whether to save the changes</param>
        void Remove(T entity, bool saveChanges = false);

        /// <summary>
        /// Saves the changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();
    }
}
