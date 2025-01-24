using AutoMapper;
using Backend.Database;
using Backend.DTO;
using Backend.Models;
using Backend.Services.Interfaces;
using MongoDB.Driver;

namespace Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Product> _products; 
        private readonly IMapper _mapper;

        public OrderService(MongoDbContext context, IMapper mapper)
        {
            _orders = context.Orders;
            _products = context.Products; 
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _orders.Find(_ => true).ToListAsync();
            var orderDTOs = new List<OrderDTO>();

            foreach (var order in orders)
            {
                var product = await _products.Find(p => p.Id == order.ProductId).FirstOrDefaultAsync();
                if (product != null)
                {
                    order.Product = product; 
                }

                var orderDTO = _mapper.Map<OrderDTO>(order);
                orderDTOs.Add(orderDTO);
            }

            return orderDTOs;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(string id)
        {
            var order = await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null) return null;

            var product = await _products.Find(p => p.Id == order.ProductId).FirstOrDefaultAsync();
            if (product != null)
            {
                order.Product = product;
            }

            return _mapper.Map<OrderDTO>(order); 
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO); 
            order.OrderDate = DateTime.UtcNow;

            await _orders.InsertOneAsync(order); 
            return await GetOrderByIdAsync(order.Id); 
        }

        public async Task UpdateOrderAsync(string id, OrderDTO orderDTO)
        {
            var existingOrder = await _orders.Find(o => o.Id == id).FirstOrDefaultAsync() ?? throw new KeyNotFoundException("Order not found.");
            _mapper.Map(orderDTO, existingOrder);

            await _orders.ReplaceOneAsync(o => o.Id == id, existingOrder); 
        }

        public async Task DeleteOrderAsync(string id)
        {
            await _orders.DeleteOneAsync(o => o.Id == id);
        }
    }
}
