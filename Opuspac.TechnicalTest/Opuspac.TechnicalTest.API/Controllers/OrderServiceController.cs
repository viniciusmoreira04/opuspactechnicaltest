using Microsoft.AspNetCore.Mvc;
using Opuspac.TechnicalTest.API.Attributes;
using Opuspac.TechnicalTest.API.Services;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;

namespace Opuspac.TechnicalTest.API.Controllers;

[Route("api/orders")]
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
        OrderServiceDTO orderServiceRabbitMQ = await RabbitMQServices.ConsumerRabbitMQ();

        OrderServiceDTO result = await _orderService.CreateOrderServiceAsync(orderServiceRabbitMQ);

        return result;
    }

    [HttpGet]
    public async Task<ActionResult<List<Domain.OrderService>>> GetOrderServices()
    {
        List<Domain.OrderService> result = await _orderService.GetAllOrderServicesAsync();

        return result ?? new List<Domain.OrderService>();
    }
}