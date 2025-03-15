using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderServiceRepository _orderServiceRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderServiceRepository orderServiceRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderServiceRepository = orderServiceRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderServiceDTO> CreateOrderServiceAsync(OrderServiceDTO orderServiceDTO)
        {
            User user = await _userRepository.GetUserByUserNameAsync(orderServiceDTO.UserName);
            Product product = await _productRepository.GetProductByDescription(orderServiceDTO.ProductDescription);

            Domain.OrderService orderService = new()
            {
                UserName = user.Name,
                ProductDescription = product.Description,
                CreatedAt = DateTime.Now
            };

            await _orderServiceRepository.InsertAsync(orderService);

            return orderServiceDTO;
        }

        public async Task<List<Domain.OrderService>> GetAllOrderServicesAsync()
        {
            var allOrders = await _orderServiceRepository.GetAllAsync();

            return allOrders.Select(p => new Domain.OrderService
            {
                Id = p.Id,
                ProductDescription = p.ProductDescription,
                UserName = p.UserName,
                CreatedAt = p.CreatedAt
            }).ToList();
        }
    }
}
