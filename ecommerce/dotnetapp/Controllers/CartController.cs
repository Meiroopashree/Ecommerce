using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult<object>> GetCart()
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return NotFound();
            }

            var totalPrice = cart.GetTotalPrice();

            return new
            {
                Cart = cart,
                TotalPrice = totalPrice
            };
        }

        // POST: api/cart/{productId}
        [HttpPost("{productId}")]
        public async Task<ActionResult<object>> AddToCart(int productId, [FromQuery] int quantity = 1)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new Cart();
                _context.Carts.Add(cart);
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Product = product
                };
                cart.Items.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();

            var totalPrice = cart.GetTotalPrice();

            return new
            {
                Cart = cart,
                TotalPrice = totalPrice
            };
        }

        // DELETE: api/cart/{productId}
        [HttpDelete("{productId}")]
        public async Task<ActionResult> RemoveFromCart(int productId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                return NotFound();
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem == null)
            {
                return NotFound();
            }

            cart.Items.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/cart/item/{cartItemId}/quantity/{quantity}
        [HttpPut("item/{cartItemId}/quantity/{quantity}")]
        public async Task<IActionResult> UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
