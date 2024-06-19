// Models/CartItem.cs
namespace dotnetapp.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }
    }
}
