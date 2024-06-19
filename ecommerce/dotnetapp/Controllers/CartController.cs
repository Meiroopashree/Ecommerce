// Controllers/CartController.cs
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private static List<Cart> carts = new List<Cart>();

        [HttpGet("{id}")]
        public ActionResult<Cart> GetCart(int id)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("{cartId}/add")]
        public ActionResult AddToCart(int cartId, [FromBody] CartItem item)
        {
            var cart = carts.FirstOrDefault(c => c.CartId == cartId);
            if (cart == null)
            {
                cart = new Cart { CartId = cartId };
                carts.Add(cart);
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(item);
            }

            return Ok(cart);
        }
    }
}
