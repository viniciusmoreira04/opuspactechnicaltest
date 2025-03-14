using Opuspac.TechnicalTest.Application.DTOs;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface IUserService
{
    Task<UserDTO> CreateUserAsync(string name, string email,  string password);
    Task<UserDTO> AuthUserAsync(string email, string password);
}
