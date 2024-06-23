// Models/CartItem.cs
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public decimal GetTotalPrice()
        {
            return Items.Sum(item => item.Product.Price * item.Quantity);
        }
    }
}
