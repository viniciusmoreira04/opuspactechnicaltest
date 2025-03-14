using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Interfaces;

public interface IOrderService
{
    Task<OrderService> CreateOrderServiceAsync(OrderServiceDTO orderServiceDTO);

    Task<List<OrderServiceDTO>> GetAllOrderServicesAsync();
}