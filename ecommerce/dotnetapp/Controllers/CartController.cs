// Controllers/CartController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CartController : ControllerBase
    {
        private readonly IUserService _userService;
        private static List<Cart> carts = new List<Cart>();

        public CartController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/cart/{id}
        [HttpGet("{id}")]
        public ActionResult<Cart> GetCart(int id)
        {
            // Get userId from token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = carts.FirstOrDefault(c => c.CartId == id && c.UserId == userId);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // POST api/cart/add
        [HttpPost("add")]
        public ActionResult AddToCart([FromBody] CartProduct cartProduct)
        {
            // Validate quantity
            if (cartProduct.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            // Get userId from token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = GenerateUniqueCartId(), // Generate a unique cartId
                    UserId = userId,
                    Items = new List<CartProduct>()
                };
                carts.Add(cart);
            }

            var existingProduct = cart.Items.FirstOrDefault(item => item.Product.ProductId == cartProduct.Product.ProductId);

            if (existingProduct != null)
            {
                existingProduct.Quantity += cartProduct.Quantity;
            }
            else
            {
                cart.Items.Add(cartProduct);
            }

            return Ok(cart);
        }

        // Simulated method to generate a unique cartId (replace with actual logic)
        private int GenerateUniqueCartId()
        {
            return carts.Count + 1; // Simulated unique cartId
        }
    }
}
