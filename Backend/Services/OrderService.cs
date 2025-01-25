using AutoMapper;
using Backend.DTO;
using Backend.Models;
using Backend.Services.Interfaces;
using Backend.Shared;
using MongoDB.Driver;

public class OrderService : IOrderService
{
    private readonly IMongoCollection<Order> _orders;
    private readonly IMapper _mapper;

    public OrderService(IMongoDatabase database, IMapper mapper)
    {
        _orders = database.GetCollection<Order>("Orders");
        _mapper = mapper;
    }

    public async Task<Result<OrderDTO>> CreateOrderAsync(OrderDTO orderDTO)
    {
        try
        {
            var order = _mapper.Map<Order>(orderDTO);
            await _orders.InsertOneAsync(order);
            var resultDTO = _mapper.Map<OrderDTO>(order);
            return Result<OrderDTO>.Success(resultDTO);
        }
        catch (Exception ex)
        {
            return Result<OrderDTO>.Failure($"Erreur lors de la création de la commande : {ex.Message}");
        }
    }

    public async Task<Result<OrderDTO>> GetOrderByIdAsync(string id)
    {
        try
        {
            var order = await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null)
            {
                return Result<OrderDTO>.Failure("Commande non trouvée");
            }
            var orderDTO = _mapper.Map<OrderDTO>(order);
            return Result<OrderDTO>.Success(orderDTO);
        }
        catch (Exception ex)
        {
            return Result<OrderDTO>.Failure($"Erreur lors de la récupération de la commande : {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<OrderDTO>>> GetAllOrdersAsync()
    {
        try
        {
            var orders = await _orders.Find(o => true).ToListAsync();
            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            return Result<IEnumerable<OrderDTO>>.Success(orderDTOs);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<OrderDTO>>.Failure($"Erreur lors de la récupération des commandes : {ex.Message}");
        }
    }

    public async Task<Result<OrderDTO>> UpdateOrderAsync(string id, OrderDTO orderDTO)
    {
        try
        {
            var order = _mapper.Map<Order>(orderDTO);
            var result = await _orders.ReplaceOneAsync(o => o.Id == id, order);
            if (result.IsAcknowledged)
            {
                var updatedOrderDTO = _mapper.Map<OrderDTO>(order);
                return Result<OrderDTO>.Success(updatedOrderDTO);
            }
            return Result<OrderDTO>.Failure("Commande non trouvée ou échec de la mise à jour");
        }
        catch (Exception ex)
        {
            return Result<OrderDTO>.Failure($"Erreur lors de la mise à jour de la commande : {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteOrderAsync(string id)
    {
        try
        {
            var result = await _orders.DeleteOneAsync(o => o.Id == id);
            if (result.IsAcknowledged)
            {
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Commande non trouvée ou échec de la suppression");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure($"Erreur lors de la suppression de la commande : {ex.Message}");
        }
    }
}
