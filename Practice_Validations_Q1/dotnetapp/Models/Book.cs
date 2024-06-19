using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [UniqueTitle(ErrorMessage = "Title must be unique")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Published date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [PastDate(ErrorMessage = "Published date must be in the past")]
        public DateTime PublishedDate { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
    }
}
