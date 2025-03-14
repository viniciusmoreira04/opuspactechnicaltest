using Opuspac.TechnicalTest.Application.DTOs;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface ITokenService 
{
    string GenerateToken(UserDTO userDTO);
    UserDTO ValidateToken(string token);
}