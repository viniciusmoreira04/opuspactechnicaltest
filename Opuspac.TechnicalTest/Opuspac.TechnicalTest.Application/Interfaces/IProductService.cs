using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface IProductService 
{
    Task<ProductDTO> CreateProductAsync(string name, string description,  decimal price);

    Task<List<Product>> GetAllProductsAsync();
}