using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface IProductService
{
    Task<Product> CreateProductAsync(string name, string description, decimal price);

    Task<List<Product>> GetAllProductsAsync();
}