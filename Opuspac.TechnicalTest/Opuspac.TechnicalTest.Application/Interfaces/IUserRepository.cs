using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByUserNameAsync(string userName);

}
