using Npgsql;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Infrastructure
{
    public class OrderServiceRepository : PostgreRepository<OrderService>, IOrderServiceRepository
    {
        public OrderServiceRepository(NpgsqlConnection context) : base(context)
        {
        }
    }
}
