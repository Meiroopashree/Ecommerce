using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Services;
using System.Security.Claims;
using Microsoft.Extensions.Logging; // Ensure this namespace is imported

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly ILogger<CartController> _logger; // Inject ILogger

        public CartController(ApplicationDbContext context, IUserService userService, ILogger<CartController> logger)
        {
            _context = context;
            _userService = userService;
            _logger = logger; // Initialize logger instance
        }

        // GET api/cart/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCart(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.CartId == id && c.UserId == userId);

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

    // UserId should not be null here due to [Authorize] attribute
    if (userId == null)
    {
        return Unauthorized("User not authenticated.");
    }

    // Find or create a cart for the current user
    var cart = await _context.Carts
        .Include(c => c.Items)
        .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
    {
        cart = new Cart
        {
            UserId = userId, // Assign the UserId here
            Items = new List<CartProduct>()
        };
        _context.Carts.Add(cart);
    }

    // Ensure the product exists in the database (check ProductId)
    var existingProduct = await _context.Products
        .FirstOrDefaultAsync(p => p.ProductId == cartProduct.Product.ProductId);

    if (existingProduct == null)
    {
        // If product doesn't exist, return a meaningful error response
        return BadRequest("Product not found.");
    }

    // Add the cart product
    var cartItem = new CartProduct
    {
        Product = existingProduct,
        Quantity = cartProduct.Quantity
    };

    cart.Items.Add(cartItem);

    try
    {
        await _context.SaveChangesAsync();
        return Ok(cart);
    }
    catch (DbUpdateException ex)
    {
        // Log the exception and return a detailed error message
        _logger.LogError(ex, "Error occurred while saving cart.");
        return StatusCode(500, $"Error saving cart: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
    }
}



        // PUT api/cart/update/{productId}
        [HttpPut("update/{productId}")]
        public async Task<ActionResult> UpdateCartItem(int productId, [FromBody] CartProduct updatedCartItem)
        {
            if (updatedCartItem.Quantity <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

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

            try
            {
                await _context.SaveChangesAsync();
                return Ok(cart);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception and return a detailed error message
                _logger.LogError(ex, "Error occurred while updating cart item.");
                return StatusCode(500, $"Error updating cart item: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // DELETE api/cart/remove/{productId}
        [HttpDelete("remove/{productId}")]
        public async Task<ActionResult> RemoveCartItem(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

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

            try
            {
                await _context.SaveChangesAsync();
                return Ok(cart);
            }
            catch (DbUpdateException ex)
            {
                // Log the exception and return a detailed error message
                _logger.LogError(ex, "Error occurred while removing cart item.");
                return StatusCode(500, $"Error removing cart item: {ex.Message}. Inner Exception: {ex.InnerException?.Message}");
            }
        }

        // Simulated method to generate a unique cartId (replace with actual logic)
        private int GenerateUniqueCartId()
        {
            return _context.Carts.Count() + 1; // Generate unique cartId based on current count in database
        }
    }
}
