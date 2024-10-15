namespace CleanRazor.Data
{
    public interface IEntity<TKey>
    {
        /// <summary>
        /// The entity identifier
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        TKey Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this entity is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this entity is enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsEnabled { get; set; }
    }
}
