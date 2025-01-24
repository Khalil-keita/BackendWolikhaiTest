using Backend.DTO;

namespace Backend.Services.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO> GetOrderByIdAsync(string id);
        Task<OrderDTO> CreateOrderAsync(OrderDTO orderDTO);
        Task UpdateOrderAsync(string id, OrderDTO orderDTO);
        Task DeleteOrderAsync(string id);
    }
}
