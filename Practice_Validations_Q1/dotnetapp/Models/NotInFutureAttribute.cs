using System;
using System.ComponentModel.DataAnnotations;
using dotnetapp.Data;
using dotnetapp.Models;

namespace dotnetapp.Models
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        
        // protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        // {
        //     var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
        //     var employee = validationContext.ObjectInstance as Employee;

        //     if (dbContext.Employees.Any(e => e.Email == value.ToString() && e.Id != employee.Id))
        //     {
        //         return new ValidationResult(ErrorMessage ?? "The email must be unique.");
        //     }

        //     return ValidationResult.Success;
        // }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
{
    var dbContext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
    var employee = validationContext.ObjectInstance as Employee;

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

    // Null check for employee
    if (employee == null)
    {
        // Handle the case where employee is null
        return new ValidationResult("Employee data is not available.");
    }

    // Check for duplicate email if dbContext.Employees is not null
    if (dbContext.Employees != null && dbContext.Employees.Any(e => e.Email == value.ToString() && e.Id != employee.Id))
    {
        return new ValidationResult(ErrorMessage ?? "The email must be unique.");
    }

    return ValidationResult.Success;
}

    }

    public class MinAgeAttribute : ValidationAttribute
    {
        private int _minAge;

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
                return new ValidationResult(this.ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}