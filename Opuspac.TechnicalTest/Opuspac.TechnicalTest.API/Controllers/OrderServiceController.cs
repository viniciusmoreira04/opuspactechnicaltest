using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Opuspac.TechnicalTest.API.Attributes;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Application.Services;
using Opuspac.TechnicalTest.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Opuspac.TechnicalTest.Portal.Server.Controllers;

[Route("api/orderservices")]
[ApiController]
[Authorize]
public class OrderServiceController : ControllerBase
{
    private readonly ILogger<OrderServiceController> _logger;
    private readonly IOrderService _orderService;

    public OrderServiceController(ILogger<OrderServiceController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<OrderServiceDTO> CreateOrderService()
    {
        OrderServiceDTO orderServiceRabbitMQ = await ConsumerRabbitMQ();

        OrderServiceDTO result = await _orderService.CreateOrderServiceAsync(orderServiceRabbitMQ);

        return result;
    }

    [HttpGet]
    public async Task<ActionResult<List<Domain.OrderService>>> GetOrderServices()
    {
        List<Domain.OrderService> result = await _orderService.GetAllOrderServicesAsync();

        return result ?? new List<Domain.OrderService>();
    }

    public static async Task<OrderServiceDTO> ConsumerRabbitMQ()
    {
        OrderServiceDTO orderService = null;

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
            await channel.QueueDeclareAsync(queue: "opuspac_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
            arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                orderService = JsonConvert.DeserializeObject<OrderServiceDTO>(message);
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(queue: AppSettingsHelper.GetRabbitMQQueueName(),
                                 autoAck: true,
                                 consumer: consumer);
        }

        return orderService;
    }
}