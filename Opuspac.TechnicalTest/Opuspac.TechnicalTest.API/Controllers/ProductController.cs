using Microsoft.AspNetCore.Mvc;
using Opuspac.TechnicalTest.API.Attributes;
using Opuspac.TechnicalTest.API.Services;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.API.Controllers;

[Route("api/products")]
[ApiController]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;
    private readonly IUserRepository _userRepository;

    public ProductController(ILogger<ProductController> logger, IProductService productService, IUserRepository userRepository)
    {
        _logger = logger;
        _productService = productService;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<Product> CreateProduct(CreateProductDTO createProductDTO)
    {
        if (!HttpContext.Items.TryGetValue("User", out object userObject) || userObject is not UserDTO userDTO)
            throw new Exception("Usuário não autenticado");

        User user = await _userRepository.GetUserByEmailAsync(userDTO.Email) ??
            throw new Exception("Usuário não encontrado");

        Product result = await _productService.CreateProductAsync(createProductDTO.Name, createProductDTO.Description, createProductDTO.Price);

        var orderService = new OrderService
        {
            UserName = user.Name,
            ProductDescription = result.Description,
            Price = result.Price,
            CreatedAt = DateTime.Now
        };

        RabbitMQServices.PublishRabbitMQ(orderService);

        return result;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        List<Product> result = await _productService.GetAllProductsAsync();

        return result ?? new List<Product>();
    }
}