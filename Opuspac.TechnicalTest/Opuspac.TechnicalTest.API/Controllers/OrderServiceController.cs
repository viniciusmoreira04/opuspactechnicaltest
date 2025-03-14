using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Application.Services;
using Opuspac.TechnicalTest.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace Opuspac.TechnicalTest.Portal.Server.Controllers;

[Route("/api/orderservices")]
[ApiController]
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
    public async Task<Domain.OrderService> CreateOrderService()
    {
        OrderServiceDTO orderServiceRabbitMQ = await ConsumerRabbitMQ();

        Domain.OrderService result = await _orderService.CreateOrderServiceAsync(orderServiceRabbitMQ);

        return result;
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
            await channel.QueueDeclareAsync(queue: "minhaFila",
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
                                 autoAck: false,
                                 consumer: consumer);
        }

        return orderService;
    }
}