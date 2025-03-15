using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Opuspac.TechnicalTest.API.Attributes;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;
using RabbitMQ.Client;
using System.Text;

namespace Opuspac.TechnicalTest.Portal.Server.Controllers;

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
            CreatedAt = DateTime.Now
        };

        PublishRabbitMQ(orderService);

        return result;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        List<Product> result = await _productService.GetAllProductsAsync();

        return result ?? new List<Product>();
    }

    public static async void PublishRabbitMQ(OrderService orderSevice)
    {
        var factory = new ConnectionFactory()
        {
            HostName = AppSettingsHelper.GetRabbitMQHostName(),
            Port = AppSettingsHelper.GetRabbitMQPort(),
            UserName = AppSettingsHelper.GetRabbitMQUserName(),
            Password = AppSettingsHelper.GetRabbitMQPassword(),
            VirtualHost = "/"
        };

        using (var connection = await factory.CreateConnectionAsync())
        using (var channel = await connection.CreateChannelAsync())
        {
            await channel.QueueDeclareAsync(queue: "minhaFila",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            string message = JsonConvert.SerializeObject(orderSevice);
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(
            exchange: AppSettingsHelper.GetRabbitMQExchange(),
            routingKey: AppSettingsHelper.GetRabbitMQRoutingKey(),
            body: new ReadOnlyMemory<byte>(body)
        );
        }
    }
}