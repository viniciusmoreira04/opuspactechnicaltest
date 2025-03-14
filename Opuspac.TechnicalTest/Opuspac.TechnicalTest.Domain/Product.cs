using System.ComponentModel.DataAnnotations.Schema;

namespace Opuspac.TechnicalTest.Domain
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
            
        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
