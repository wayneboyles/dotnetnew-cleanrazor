using CleanRazor.Entities;

namespace CleanRazor.Dtos
{
    public abstract class EntityDto<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; } = default!;

        public bool IsEnabled { get; set; }
    }
}