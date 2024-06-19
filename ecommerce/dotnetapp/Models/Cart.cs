// Models/CartItem.cs
using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public List<CartProduct> Items { get; set; } = new List<CartProduct>();

        public decimal TotalPrice
        {
            get
            {
                return Items.Sum(item => item.Product.Price * item.Quantity);
            }
        }
    }
}
