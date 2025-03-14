using MongoDB.Driver;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Infrastructure
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase context) : base(context)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var get = await Collection.FindAsync(x => x.Email == email);

            return await get.FirstOrDefaultAsync();
        }
    }
}
