using Microsoft.AspNetCore.Identity;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly CounterService _counterService;

        public UserService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository, CounterService counterService)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _counterService = counterService;
        }

        public async Task<UserDTO> CreateUserAsync(string name, string email, string password)
        {
            User? user = await _userRepository.GetUserByEmailAsync(email);

            if (user is not null)
                throw new Exception("Já existe esse usuário");

            user = new()
            {
                Id = await _counterService.GetNextSequenceValueAsync("User"),
                Name = name,
                Email = email,
                HashedPassword = _passwordHasher.HashPassword(user, password),
                CreatedAt = DateTime.Now
            };

            await _userRepository.InsertAsync(user);

            return new UserDTO()
            {
                Name = user.Name,
                Email = user.Email
            };
        }

        public async Task<UserDTO> AuthUserAsync(string email, string password)
        {
            User user = await _userRepository.GetUserByEmailAsync(email) ??
                throw new Exception("Autenticação falhou");

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Autenticação falhou");

            return new UserDTO()
            {
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
