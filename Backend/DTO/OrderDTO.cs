namespace Backend.DTO
{
    public class OrderProductDTO
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderDTO
    {
        public List<OrderProductDTO> Products { get; set; }
        public decimal Montant { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
