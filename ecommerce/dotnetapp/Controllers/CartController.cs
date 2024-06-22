using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Services;
using System.Security.Claims;


namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public CartController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        /// GET api/cart/{id}
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

    // Find or create a cart for the current user
    var cart = await _context.Carts
        .Include(c => c.Items)
        .FirstOrDefaultAsync(c => c.UserId == userId);

    if (cart == null)
    {
        cart = new Cart
        {
            UserId = userId,
            Items = new List<CartProduct>()
        };
        _context.Carts.Add(cart);
    }

    // Ensure the product exists in the database (check ProductId)
    var existingProduct = await _context.Products
        .FirstOrDefaultAsync(p => p.ProductId == cartProduct.Product.ProductId);

    if (existingProduct == null)
    {
        // If product doesn't exist, you may want to handle this case according to your application logic
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
        // Log the exception or handle it appropriately
        return StatusCode(500, "Error saving cart.");
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

    await _context.SaveChangesAsync();

    return Ok(cart);
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

    await _context.SaveChangesAsync();

    return Ok(cart);
}


        // Simulated method to generate a unique cartId (replace with actual logic)
        private int GenerateUniqueCartId()
        {
            return _context.Carts.Count() + 1; // Generate unique cartId based on current count in database
        }
    }
}
