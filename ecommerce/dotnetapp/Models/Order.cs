using System;
using System.Collections.Generic;

namespace dotnetapp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        // Add other necessary properties (e.g., customer details, payment status)
    }
}