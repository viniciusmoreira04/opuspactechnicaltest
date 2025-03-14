using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;
using RabbitMQ.Client;
using System.Text;

namespace Opuspac.TechnicalTest.Portal.Server.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpPost]
    public async Task<ProductDTO> CreateProduct(CreateProductDTO createProductDTO)
    {
        ProductDTO result = await _productService.CreateProductAsync(createProductDTO.Name, createProductDTO.Description, createProductDTO.Price);

        var orderServiceDTO = new OrderServiceDTO
        {
            User = createProductDTO.User,
            Product = result,
            Date = DateTime.Now
        };

        PublishRabbitMQ(orderServiceDTO);

        return result;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        List<Product> result = await _productService.GetAllProductsAsync();

        return result ?? new List<Product>();
    }

    public static async void PublishRabbitMQ(OrderServiceDTO orderSeviceDTO)
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

                string message = JsonConvert.SerializeObject(orderSeviceDTO);
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(
                exchange: AppSettingsHelper.GetRabbitMQExchange(),
                routingKey: AppSettingsHelper.GetRabbitMQRoutingKey(),
                body: new ReadOnlyMemory<byte>(body)
            );
        }
    }
}