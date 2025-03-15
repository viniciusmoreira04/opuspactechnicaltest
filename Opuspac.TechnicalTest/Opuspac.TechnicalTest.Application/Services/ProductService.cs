using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepositor)
        {
            _productRepository = productRepositor;
        }

        public async Task<Product> CreateProductAsync(string name, string description, decimal price)
        {
            Product product = new()
            {
                Name = name,
                Description = description,
                Price = price,
                CreatedAt = DateTime.Now
            };

            await _productRepository.InsertAsync(product);

            return new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var allProducts = await _productRepository.GetAllAsync();

            return allProducts.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description
            }).ToList();
        }
    }
}
