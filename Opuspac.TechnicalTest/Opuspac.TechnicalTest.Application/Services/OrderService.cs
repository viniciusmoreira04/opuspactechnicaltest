using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Application.Interfaces;
using Opuspac.TechnicalTest.Domain;

namespace Opuspac.TechnicalTest.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderServiceRepository _orderServiceRepository;
        private readonly IPostgreConnection<Domain.OrderService> _postgreConnection;
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderServiceRepository orderServiceRepository, IPostgreConnection<Domain.OrderService> postgreConnection, IUserRepository userRepository)
        {
            _orderServiceRepository = orderServiceRepository;
            _postgreConnection = postgreConnection;
            _userRepository = userRepository;
        }

        public async Task<Domain.OrderService> CreateOrderServiceAsync(OrderServiceDTO orderServiceDTO)
        {
            User user = await _userRepository.GetUserByEmailAsync(orderServiceDTO.User.Email);
            Product product = await _postgreConnection.GetProductAsync(orderServiceDTO.Product.Description);

            Domain.OrderService orderService = new()
            {
                UserId = user.Id,
                ProductId = product.Id,
                CreatedAt = DateTime.Now
            };

            await _postgreConnection.InsertAsync(orderService);

            return orderService;
        }

        public Task<List<OrderServiceDTO>> GetAllOrderServicesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
