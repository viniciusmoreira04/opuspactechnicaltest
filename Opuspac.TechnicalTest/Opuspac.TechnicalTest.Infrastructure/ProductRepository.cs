using MongoDB.Driver;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Infrastructure
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDatabase context) : base(context)
        {
        }

        public async Task<Product?> GetProductByDescription(string Description)
        {
            var get = await Collection.FindAsync(x => x.Description == Description);

            return await get.FirstOrDefaultAsync();
        }
    }
}
