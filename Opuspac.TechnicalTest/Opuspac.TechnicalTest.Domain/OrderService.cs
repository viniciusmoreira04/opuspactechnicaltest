namespace Opuspac.TechnicalTest.Domain
{
    public class OrderService : BaseEntity
    {
        public string UserName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
    }
}
