namespace dotnetapp.Models
{
    public class Product
    {   
        public int ProductId { get; set; } // Unique identifier for the product
        public string Name { get; set; } // Name of the product
        public string Description { get; set; } // Description of the product
        public decimal Price { get; set; } // Price of the product
        public string Category { get; set; } // Category of the product
        public int StockQuantity { get; set; } // Quantity of the product in stock
        public string ImageUrl { get; set; } // URL of the product image
    }
}
