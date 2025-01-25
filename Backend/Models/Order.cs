using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Backend.Models
{

    public class Order
    {
        public string Id { get; set; }
        public List<OrderProduct> Products { get; set; }
        public decimal Montant { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class OrderProduct
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
