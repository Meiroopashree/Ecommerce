using System;
using System.Collections.Generic;

namespace dotnetapp.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; } // Include Product entity
    }
}