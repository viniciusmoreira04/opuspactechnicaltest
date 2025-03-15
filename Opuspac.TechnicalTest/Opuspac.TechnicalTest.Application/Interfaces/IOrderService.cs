using Opuspac.TechnicalTest.Application.DTOs;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface IOrderService
{
    Task<OrderServiceDTO> CreateOrderServiceAsync(OrderServiceDTO orderServiceDTO);

    Task<List<Domain.OrderService>> GetAllOrderServicesAsync();
}