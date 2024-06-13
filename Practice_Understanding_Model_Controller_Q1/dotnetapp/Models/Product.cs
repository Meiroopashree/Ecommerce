using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the product name.")]
        [StringLength(100, ErrorMessage = "Name should not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please specify the product category.")]
        [StringLength(50, ErrorMessage = "Category should not exceed 50 characters.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Please enter the product price.")]
        [Range(0.01, 10000, ErrorMessage = "Price should be between 0.01 and 10,000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please enter the quantity.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity should be a non-negative number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please specify the manufacturer.")]
        [StringLength(100, ErrorMessage = "Manufacturer should not exceed 100 characters.")]
        public string Manufacturer { get; set; }

    }
}
