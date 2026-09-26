using System.ComponentModel.DataAnnotations;

namespace CoffeeShopApi.DTOs
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Ім'я клієнта є обов'язковим")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ім'я має містити від 2 до 50 символів")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Кошик не може бути порожнім")]
        [MinLength(1, ErrorMessage = "Додайте хоча б один товар")]
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, 100, ErrorMessage = "Кількість товару має бути від 1 до 100")]
        public int Quantity { get; set; }
    }
}