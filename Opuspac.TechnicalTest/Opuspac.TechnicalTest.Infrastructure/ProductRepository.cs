using Dapper;
using Npgsql;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Infrastructure
{
    public class ProductRepository : PostgreRepository<Product>, IProductRepository
    {
        public ProductRepository(NpgsqlConnection context) : base(context)
        {
        }

        public async Task<Product?> GetProductByDescription(string description)
        {
            string tableName = typeof(Product).Name.ToLower();
            string query = $"SELECT * FROM {tableName} WHERE Description = @Description";
            return await Connection.QueryFirstOrDefaultAsync<Product>(query, new { Description = description });
        }
    }
}
