using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dotnetapp.Data;

namespace dotnetapp.Models
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;

            // Check if the value is a valid DateTime
            if (value is DateTime)
            {
                date = (DateTime)value;

                // Compare the date with today's date
                if (date >= DateTime.Today)
                {
                    return new ValidationResult("Published date must be in the past");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class UniqueTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var book = validationContext.ObjectInstance as Book;

            // Null check for dbContext
            if (dbContext == null)
            {
                return new ValidationResult("Database context is not available");
            }

            // Null check for value
            if (value == null)
            {
                return new ValidationResult("Title is required");
            }

            // Null check for book
            if (book == null)
            {
                return new ValidationResult("Book data is not available");
            }

            // Check for duplicate title if dbContext.Books is not null
            if (dbContext.Books != null && dbContext.Books.Any(b => b.Title == value.ToString() && b.BookId != book.BookId))
            {
                return new ValidationResult(ErrorMessage ?? "Title must be unique");
            }

            return ValidationResult.Success;
        }
    }
}
