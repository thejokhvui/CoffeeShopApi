namespace CoffeeShopApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ItemsSummary { get; set; } = string.Empty; 
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Прийнято";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}