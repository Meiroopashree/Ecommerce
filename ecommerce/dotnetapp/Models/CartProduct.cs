using System.Collections.Generic;
using System.Linq;

namespace dotnetapp.Models
{
public class CartProduct
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}