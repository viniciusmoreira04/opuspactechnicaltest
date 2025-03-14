﻿using MongoDB.Driver;
using Npgsql;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected IMongoCollection<T> Collection { get; }
        protected NpgsqlConnection Connection { get; set; }

        public Repository(IMongoDatabase context)
        {
            Collection = context.GetCollection<T>(typeof(T).Name);
            Connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=opuspactest;User Id=postgres;Password=123;");
            Connection.Open();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var getAll = await Collection.FindAsync(_ => true);

            return await getAll.ToListAsync();
        }

        public async Task<T?> GetAsync(long id)
        {
            var get = await Collection.FindAsync(x => x.Id == id);

            return await get.FirstOrDefaultAsync();
        }

        public async Task InsertAsync(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            await Collection.InsertOneAsync(entity);
        }

        public Task RemoveAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
