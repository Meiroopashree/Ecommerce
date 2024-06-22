using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Data;
using dotnetapp.Models;
using System.Security.Claims; 
using Microsoft.AspNetCore.Authorization; 


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

            // GET api/cart/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Cart>> GetCart(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var cart = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.CartId == id && c.UserId == userId);

        if (cart == null)
        {
            return NotFound();
        }

        return Ok(cart);
    }

    // POST api/cart/add
    [HttpPost("add")]
    public async Task<ActionResult<Cart>> AddToCart([FromBody] CartProduct cartProduct)
    {
        if (cartProduct.Quantity <= 0)
        {
            return BadRequest("Quantity must be greater than zero.");
        }

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                CartId = GenerateUniqueCartId(),
                UserId = userId,
                Items = new List<CartProduct>()
            };
            _context.Carts.Add(cart);
        }

        var existingProduct = cart.Items.FirstOrDefault(item => item.ProductId == cartProduct.ProductId);

        if (existingProduct != null)
        {
            existingProduct.Quantity += cartProduct.Quantity;
        }
        else
        {
            var product = await _context.Products.FindAsync(cartProduct.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            cart.Items.Add(new CartProduct
            {
                CartId = cart.CartId, // Assuming CartId is part of CartProduct model
                ProductId = product.ProductId,
                Quantity = cartProduct.Quantity,
                Product = product
            });
        }

        await _context.SaveChangesAsync();

        return Ok(cart);
    }


        // PUT api/cart/update/{productId}
        [HttpPut("update/{productId}")]
        public async Task<ActionResult<Cart>> UpdateCartItem(int productId, [FromBody] CartProduct updatedCartItem)
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

            var existingProduct = cart.Items.FirstOrDefault(item => item.ProductId == productId);

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
        public async Task<ActionResult<Cart>> RemoveCartItem(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            var existingProduct = cart.Items.FirstOrDefault(item => item.ProductId == productId);

            if (existingProduct == null)
            {
                return NotFound("Product not found in cart.");
            }

            _context.CartProducts.Remove(existingProduct);

            await _context.SaveChangesAsync();

            return Ok(cart);
        }

        // Simulated method to generate a unique cartId (replace with actual logic)
        private int GenerateUniqueCartId()
        {
            return _context.Carts.Count() + 1; // Simulated unique cartId
        }
    }
}
