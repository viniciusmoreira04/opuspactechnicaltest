using System.ComponentModel.DataAnnotations;

namespace Opuspac.TechnicalTest.Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}