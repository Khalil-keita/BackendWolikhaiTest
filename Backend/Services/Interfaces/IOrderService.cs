using Backend.DTO;
using Backend.Shared;

namespace Backend.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Result<OrderDTO>> CreateOrderAsync(OrderDTO orderDTO);
        Task<Result<OrderDTO>> GetOrderByIdAsync(string id);
        Task<Result<IEnumerable<OrderDTO>>> GetAllOrdersAsync();
        Task<Result<OrderDTO>> UpdateOrderAsync(string id, OrderDTO orderDTO);
        Task<Result<bool>> DeleteOrderAsync(string id);
    }
}
