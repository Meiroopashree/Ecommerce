using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using dotnetapp.Data;

namespace dotnetapp.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
            var customer = validationContext.ObjectInstance as Customer;

            // Null check for dbContext
            if (dbContext == null)
            {
                // Handle the case where dbContext is null
                return new ValidationResult("Database context is not available.");
            }

            // Null check for value
            if (value == null)
            {
                // Handle the case where value is null
                return new ValidationResult("The email address is required.");
            }

            // Null check for customer
            if (customer == null)
            {
                // Handle the case where customer is null
                return new ValidationResult("Customer data is not available.");
            }

            // Check for duplicate email if dbContext.Customers is not null
            if (dbContext.Customers != null && dbContext.Customers.Any(c => c.Email == value.ToString() && c.CustomerId != customer.CustomerId))
            {
                return new ValidationResult(ErrorMessage ?? "The email must be unique.");
            }

            return ValidationResult.Success;
        }
    }

    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int _minAge;

        public MinAgeAttribute(int minAge)
        {
            _minAge = minAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateOfBirth = (DateTime)value;
            int age = DateTime.Now.Year - dateOfBirth.Year;

            if (dateOfBirth > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            if (age < _minAge)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
