using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetProductByDescription(string Description);

}
