using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface IOrderService
{
    Task<OrderServiceDTO> CreateOrderServiceAsync(OrderServiceDTO orderServiceDTO);

    Task<List<Domain.OrderService>> GetAllOrderServicesAsync();
}