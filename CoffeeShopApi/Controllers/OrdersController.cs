using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeShopApi.Data;
using CoffeeShopApi.Models;
using CoffeeShopApi.DTOs;

namespace CoffeeShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly CoffeeShopContext _context;

        public OrdersController(CoffeeShopContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newOrder = new Order
            {
                CustomerName = orderDto.CustomerName,
                Status = "New",
                OrderItems = new List<OrderItem>()
            };

            decimal calculatedTotal = 0;

            foreach (var item in orderDto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    return NotFound(new { Message = $"Товар з ID {item.ProductId} не знайдено." });

                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                calculatedTotal += item.Quantity * product.Price;
                newOrder.OrderItems.Add(orderItem);
            }

            newOrder.TotalAmount = calculatedTotal;

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.Id }, newOrder);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound(new { Message = "Замовлення не знайдено." });

            return Ok(order);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string newStatus)
        {
            var allowedStatuses = new[] { "New", "InProgress", "Completed", "Canceled" };
            if (!allowedStatuses.Contains(newStatus))
                return BadRequest(new { Message = "Неприпустимий статус замовлення." });

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound(new { Message = "Замовлення не знайдено." });

            order.Status = newStatus;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Статус успішно оновлено.", OrderId = order.Id, Status = order.Status });
        }
    }
}