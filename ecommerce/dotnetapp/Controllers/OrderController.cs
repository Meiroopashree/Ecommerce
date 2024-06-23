// Controllers/OrderController.cs
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/order
        [HttpPost]
        public async Task<ActionResult<Order>> PlaceOrder(Order order)
        {
            try
            {
                // Include product details in order items
                foreach (var item in order.Items)
                {
                    item.Product = await _context.Products.FindAsync(item.ProductId);
                    if (item.Product == null)
                    {
                        return NotFound($"Product with ID {item.ProductId} not found");
                    }
                }

                // Calculate total price based on order items
                order.TotalPrice = CalculateTotalPrice(order.Items);

                // Set order date
                order.OrderDate = DateTime.UtcNow;

                // Add order to context and save changes
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return Ok(order); // Return the saved order with HTTP 200 OK status
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Helper method to calculate total price based on order items
        private decimal CalculateTotalPrice(List<OrderItem> items)
        {
            decimal totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.Quantity * item.Product.Price;
            }
            return totalPrice;
        }
    }
}
