using Dapper;
using Npgsql;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;
using System.Data;

namespace Opuspac.TechnicalTest.Infrastructure
{
    public class PostgreRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected NpgsqlConnection Connection { get; set; }

        public PostgreRepository(NpgsqlConnection connection)
        {
            Connection = connection;
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public async Task<T?> GetAsync(long id)
        {
            string tableName = typeof(T).Name.ToLower();
            string query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await Connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task InsertAsync(T entity)
        {
            string tableName = typeof(T).Name.ToLower();
            var properties = typeof(T).GetProperties()
                                      .Where(p => p.Name != "Id" && p.GetValue(entity) != null);

            var columns = string.Join(", ", properties.Select(p => p.Name));
            var values = string.Join(", ", properties.Select(p => "@" + p.Name));

            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            await Connection.ExecuteAsync(query, entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            string tableName = typeof(T).Name.ToLower();
            string query = $"SELECT * FROM {tableName}";
            var result = await Connection.QueryAsync<T>(query);
            return result.AsList();
        }

        public async Task RemoveAsync(long id)
        {
            string tableName = typeof(T).Name.ToLower();
            string query = $"DELETE FROM {tableName} WHERE Id = @Id";
            await Connection.ExecuteAsync(query, new { Id = id });
        }
    }
}