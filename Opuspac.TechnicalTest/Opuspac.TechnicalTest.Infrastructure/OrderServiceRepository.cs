using MongoDB.Driver;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Infrastructure
{
    public class OrderServiceRepository : Repository<OrderService>, IOrderServiceRepository
    {
        public OrderServiceRepository(IMongoDatabase context) : base(context)
        {
        }
    }
}
