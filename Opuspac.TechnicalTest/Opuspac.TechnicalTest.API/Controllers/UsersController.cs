using Microsoft.AspNetCore.Mvc;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;

namespace Opuspac.TechnicalTest.Portal.Server.Controllers;

[Route("api/auth")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UsersController(ILogger<UsersController> logger, IUserService userService, ITokenService tokenService)
    {
        _logger = logger;
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> CreateUser(CreateUserDTO userDTO)
    {
        UserDTO result = await _userService.CreateUserAsync(userDTO.Name, userDTO.Email, userDTO.Password);

        return Ok(result);
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser(AuthUserDTO authUserDTO)
    {
        UserDTO user = await _userService.AuthUserAsync(authUserDTO.Email, authUserDTO.Password);

        string token = _tokenService.GenerateToken(user);

        return Ok(new
        {
            Token = token,
            User = user
        });
    }
}