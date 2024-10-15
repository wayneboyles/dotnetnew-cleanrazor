using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanRazor.Data
{
    public abstract class Entity<TKey> : IEntity<TKey> where TKey : class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; } = default!;

        public bool IsEnabled { get; set; }
    }
}
