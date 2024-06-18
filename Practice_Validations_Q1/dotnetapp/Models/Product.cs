using System;
using System.ComponentModel.DataAnnotations;
using dotnetapp.Models;

namespace dotnetapp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [NotInFutureAttribute(ErrorMessage = "Release date cannot be in the future")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }
    }
}
