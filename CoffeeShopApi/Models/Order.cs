namespace CoffeeShopApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "New"; 

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}