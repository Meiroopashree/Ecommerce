using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ProductController _productController; // Inject ProductController
        private static List<Cart> carts = new List<Cart>();

        public CartController(IUserService userService, ProductController productController)
        {
            _userService = userService;
            _productController = productController;
        }

        // GET api/cart/{id}
        [HttpGet("{id}")]
        public ActionResult<Cart> GetCart(int id)
        {
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
        public async Task<ActionResult> AddToCart([FromBody] CartProduct cartProduct)
        {
            if (cartProduct.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    CartId = GenerateUniqueCartId(),
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
                var productResult = await _productController.GetProduct(cartProduct.Product.ProductId);
                if (productResult.Result is NotFoundResult)
                {
                    return NotFound("Product not found.");
                }
                cartProduct.Product = productResult.Value;
                cart.Items.Add(cartProduct);
            }

            return Ok(cart);
        }

        // PUT api/cart/update/{productId}
        [HttpPut("update/{productId}")]
        public ActionResult UpdateCartItem(int productId, [FromBody] CartProduct updatedCartItem)
        {
            if (updatedCartItem.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var existingProduct = cart.Items.FirstOrDefault(item => item.Product.ProductId == productId);

            if (existingProduct == null)
            {
                return NotFound("Product not found in cart.");
            }

            existingProduct.Quantity = updatedCartItem.Quantity;

            return Ok(cart);
        }

        // DELETE api/cart/remove/{productId}
        [HttpDelete("remove/{productId}")]
        public ActionResult RemoveCartItem(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = carts.FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var existingProduct = cart.Items.FirstOrDefault(item => item.Product.ProductId == productId);

            if (existingProduct == null)
            {
                return NotFound("Product not found in cart.");
            }

            cart.Items.Remove(existingProduct);

            return Ok(cart);
        }

        // Simulated method to generate a unique cartId (replace with actual logic)
        private int GenerateUniqueCartId()
        {
            return carts.Count + 1; // Simulated unique cartId
        }
    }
}
