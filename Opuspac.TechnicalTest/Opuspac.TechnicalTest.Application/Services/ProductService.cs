using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IPostgreConnection<Product> _postgreConnection;

        public ProductService(IPostgreConnection<Product> postgreConnection)
        {
            _postgreConnection = postgreConnection;
        }

        public async Task<ProductDTO> CreateProductAsync(string name, string description, decimal price)
        {
            Product product = new()
            {
                Name = name,
                Description = description,
                Price = price,
                CreatedAt = DateTime.Now
            };

            await _postgreConnection.InsertAsync(product);

            return new ProductDTO()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var allProducts = await _postgreConnection.GetAllAsync();

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
