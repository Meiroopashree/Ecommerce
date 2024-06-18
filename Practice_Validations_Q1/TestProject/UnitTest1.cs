using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;
using dotnetapp.Models;
using System.ComponentModel.DataAnnotations;
using static NuGet.Packaging.PackagingConstants;
using System.Numerics;
using dotnetapp.Controllers;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data;
// using Your.Namespace.For.MinAge.Attribute;


namespace dotnetapp.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void Customer_FirstName_Have_RequiredAttribute()
        {
            var count = 0;

            Type customerType = typeof(Customer);
            PropertyInfo[] properties = customerType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "FirstName")
                {
                    var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                    Assert.NotNull(requiredAttribute, $"{property.Name} should have a RequiredAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Customer_LastName_Have_RequiredAttribute()
        {
            var count = 0;

            Type customerType = typeof(Customer);
            PropertyInfo[] properties = customerType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "LastName")
                {
                    var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                    Assert.NotNull(requiredAttribute, $"{property.Name} should have a RequiredAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Customer_Properties_Have_EmailAddressAttribute()
        {
            var count = 0;
            Type customerType = typeof(Customer);
            PropertyInfo[] properties = customerType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Email")
                {
                    var emailAttribute = property.GetCustomAttribute<EmailAddressAttribute>();
                    Assert.NotNull(emailAttribute, $"{property.Name} should have an EmailAddressAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
            
        }

        [Test]
        public void Customer_Properties_Have_RegularExpressionAttribute()
        {
            var count = 0;

            Type customerType = typeof(Customer);
            PropertyInfo[] properties = customerType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "PhoneNumber")
                {
                    var regexAttribute = property.GetCustomAttribute<RegularExpressionAttribute>();
                    Assert.NotNull(regexAttribute, $"{property.Name} should have a RegularExpressionAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail("PhoneNumber property does not have a RegularExpressionAttribute.");
            }
        }


        [Test]
        public void Customer_Properties_Have_DataTypeAttribute()
        {
            var count = 0;
            Type customerType = typeof(Customer);
            PropertyInfo[] properties = customerType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "BirthDate")
                {
                    var dataTypeAttribute = property.GetCustomAttribute<DataTypeAttribute>();
                    Assert.NotNull(dataTypeAttribute, $"{property.Name} should have a DataTypeAttribute.");
                    count++;
                    break;
                } 
            }
            if(count == 0)
            {
                Assert.Fail();
            }
        }

        
        [Test]
        public void Customer_Property_FirstName_Validation()
        {
            var customer1 = new Dictionary<string, object>
            {
                { "FirstName", "" },
                { "LastName", "" },
                { "Email", "a@gmail.com" },
                { "PhoneNumber", 976543210 },
                { "BirthDate", DateTime.Parse("1990-01-01") },
                { "Address", "123 Main Street, Anytown, USA" }
            };
            var customer = CreateEmpFromDictionary(customer1);
            string expectedErrorMessage = "First name is required";
            var context = new ValidationContext(customer, null, null);
            var results = new List<ValidationResult>();

            var validationContext = new ValidationContext(customer) { MemberName = "FirstName" };
            var requiredAttribute = new RequiredAttribute();

            // Act
            var validationResult = requiredAttribute.GetValidationResult(customer.FirstName, validationContext);

            // Assert
            Assert.IsNotNull(validationResult, "Validation should fail for null FirstName.");
            Assert.AreEqual("First name is required", validationResult.ErrorMessage, "Error message should match.");


            bool isValid = Validator.TryValidateObject(customer, context, results);

            if (expectedErrorMessage == null)
            {
                Assert.IsTrue(isValid);
            }
            else
            {
                Assert.IsFalse(isValid);
                var errorMessages = results.Select(result => result.ErrorMessage).ToList();
                Assert.Contains(expectedErrorMessage, errorMessages);
            }
        }

        [Test]
        public void Customer_Property_LastName_Validation()
        {
            var customer1 = new Dictionary<string, object>
            {
                { "FirstName", "" },
                { "LastName", "" },
                { "Email", "a@gmail.com" },
                { "PhoneNumber", 976543210 },
                { "BirthDate", DateTime.Parse("1990-01-01") },
                { "Address", "123 Main Street, Anytown, USA" }
            };
            var customer = CreateEmpFromDictionary(customer1);
            string expectedErrorMessage = "Last name is required";
            var context = new ValidationContext(customer, null, null);
            var results = new List<ValidationResult>();

            var validationContext = new ValidationContext(customer) { MemberName = "LastName" };
            var requiredAttribute = new RequiredAttribute();

            // Act
            var validationResult = requiredAttribute.GetValidationResult(customer.LastName, validationContext);

            // Assert
            Assert.IsNotNull(validationResult, "Validation should fail for null LastName.");
            Assert.AreEqual("Last name is required", validationResult.ErrorMessage, "Error message should match.");


            bool isValid = Validator.TryValidateObject(customer, context, results);

            if (expectedErrorMessage == null)
            {
                Assert.IsTrue(isValid);
            }
            else
            {
                Assert.IsFalse(isValid);
                var errorMessages = results.Select(result => result.ErrorMessage).ToList();
                Assert.Contains(expectedErrorMessage, errorMessages);
            }
        }

        public Customer CreateEmpFromDictionary(Dictionary<string, object> data)
        {
            var player = new Customer();
            foreach (var kvp in data)
            {
                var propertyInfo = typeof(Customer).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType == typeof(decimal) && kvp.Value is int intValue)
                    {
                        propertyInfo.SetValue(player, (decimal)intValue);
                    }
                    else
                    {
                        propertyInfo.SetValue(player, kvp.Value);
                    }
                }
            }
            return player;
        }


        [Test]
        public void Customer_Property_Email_Validation()
        {
           
            var customer1 = new Dictionary<string, object>
            {
                { "FirstName", "" },
                { "LastName", "" },
                { "Email", "a@gmail.com" },
                { "PhoneNumber", 976543210 },
                { "BirthDate", DateTime.Parse("1990-01-01") },
                { "Address", "123 Main Street, Anytown, USA" }
            };
            var customer = CreateEmpFromDictionary(customer1);
            string expectedErrorMessage = "Email is required";
            var context = new ValidationContext(customer, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(customer, context, results);

            if (expectedErrorMessage == null)
            {
                Assert.IsTrue(isValid);
            }
            else
            {
                Assert.IsFalse(isValid);
                var errorMessages = results.Select(result => result.ErrorMessage).ToList();
                Assert.Contains(expectedErrorMessage, errorMessages);
            }   
        }

        //Checking if CustomerController exists
        [Test]
        public void CustomerControllerExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.CustomerController";

            Assembly assembly = Assembly.Load(assemblyName);
            Type EmpControllerType = assembly.GetType(typeName);

            Assert.IsNotNull(EmpControllerType, "CustomerController does not exist in the assembly.");
        }

        //Checking if UniqueEmailAttribute class exists
        [Test]
        public void UniqueEmailAttributeModelExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.UniqueEmailAttribute";
            Assembly assembly = Assembly.Load(assemblyName);
            Type uniqueemailType = assembly.GetType(typeName);
            Assert.IsNotNull(uniqueemailType);
            var unique = Activator.CreateInstance(uniqueemailType);
            Assert.IsNotNull(unique);
        }

        [Test]
        public void CustomerClass_HasIDProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Customer";

            Assembly assembly = Assembly.Load(assemblyName);
            Type idType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = idType.GetProperty("CustomerId");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(int), propertyInfo.PropertyType);
        }

        //Test if Email property is present
        [Test]
        public void CustomerClass_HasEmailProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Customer";

            Assembly assembly = Assembly.Load(assemblyName);
            Type emailType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = emailType.GetProperty("Email");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        [Test]
        public void CustomerClass_HasPhoneNumberProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Customer";

            Assembly assembly = Assembly.Load(assemblyName);
            Type salaryType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = salaryType.GetProperty("PhoneNumber");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        [Test]
        public void CustomerClass_HasBirthDateProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Customer";

            Assembly assembly = Assembly.Load(assemblyName);
            Type customerType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = customerType.GetProperty("BirthDate");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DateTime), propertyInfo.PropertyType);
        }

        // Test case for Address property
        [Test]
        public void CustomerClass_HasAddressProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Customer";

            Assembly assembly = Assembly.Load(assemblyName);
            Type customerType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = customerType.GetProperty("Address");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        [Test]
        public void CustomerController_CreateMethodExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.CustomerController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Specify the parameter types for the search method
            Type[] parameterTypes = new Type[] { typeof(Customer) }; // Adjust based on your method signature

            // Find the Create method with the specified parameter types
            MethodInfo createMethod = controllerType.GetMethod("Create", parameterTypes);
            Assert.IsNotNull(createMethod);
        }

        [Test]
        public void CustomerController_SuccessMethodExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.CustomerController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Find the Success method without parameters
            MethodInfo successMethod = controllerType.GetMethod("Success", Type.EmptyTypes);
            Assert.IsNotNull(successMethod);
        }

        [Test]
        public void CustomerController_Constructor_InjectsDbContext()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.CustomerController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            var constructor = controllerType.GetConstructors().FirstOrDefault(); // Get the constructor
            var parameters = constructor?.GetParameters().FirstOrDefault(); // Check if it expects parameters
            Assert.IsNotNull(parameters);
            Assert.AreEqual(typeof(ApplicationDbContext), parameters.ParameterType); // Ensure ApplicationDbContext is injected
        }

        
        [Test]
        public void Test_CreateViewFile_Exists()
        {
            string createPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Customer/Create.cshtml");
            bool createViewExists = File.Exists(createPath);

            Assert.IsTrue(createViewExists, "Create.cshtml view file does not exist.");
        }
        [Test]
        public void Test_SuccessViewFile_Exists()
        {
            string successPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Customer/Success.cshtml");
            bool successViewExists = File.Exists(successPath);

            Assert.IsTrue(successViewExists, "Success.cshtml view file does not exist.");
        }


         private Customer CreateCustomerFromDictionary(Dictionary<string, object> data)
        {
            var customer = new Customer();
            foreach (var kvp in data)
            {
                var propertyInfo = typeof(Customer).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType == typeof(decimal) && kvp.Value is int intValue)
                    {
                        propertyInfo.SetValue(customer, (decimal)intValue);
                    }
                    else
                    {
                        propertyInfo.SetValue(customer, kvp.Value);
                    }
                }
            }
            return customer;
        }
        [Test]
        public void Customer_Properties_Have_UniqueEmailAttribute()
        {
            Type customerType = typeof(Customer);
            PropertyInfo emailProperty = customerType.GetProperty("Email");

            var uniqueEmailAttribute = emailProperty.GetCustomAttribute<UniqueEmailAttribute>();

            Assert.IsNotNull(uniqueEmailAttribute, "UniqueEmail attribute should be applied to the Email property");
        }

        [Test]
        public void Customer_Properties_Have_MinAgeAttribute()
        {
            // Arrange
            Type customerType = typeof(Customer);
            PropertyInfo dobProperty = customerType.GetProperty("BirthDate");

            // Act
            var minAgeAttribute = dobProperty.GetCustomAttribute<MinAgeAttribute>();

            // Assert
            Assert.IsNotNull(minAgeAttribute, "MinAge attribute should be applied to the BirthDate property");
        }

        [Test]
        public void MinAgeAttribute_ValidAge_ShouldPass()
        {
            var customer1 = new Dictionary<string, object>
            {
                { "FirstName", "" },
                { "LastName", "" },
                { "Email", "a@gmail.com" },
                { "PhoneNumber", 976543210 },
                { "BirthDate", DateTime.Parse("1990-01-01") },
                { "Address", "123 Main Street, Anytown, USA" }
            };
            var customer = CreateEmpFromDictionary(customer1);
            var validationContext = new ValidationContext(customer) { MemberName = "BirthDate" };
            var minAgeAttribute = new MinAgeAttribute(18);

            // Act
            var validationResult = minAgeAttribute.GetValidationResult(customer.BirthDate, validationContext);
            // Assert
            Assert.IsNull(validationResult, "Validation should pass for valid age.");
        }

         [Test]
        public void MinAgeAttribute_InvalidAge_ShouldFail()
        {
            var customer1 = new Dictionary<string, object>
            {
               { "FirstName", "" },
                { "LastName", "" },
                { "Email", "a@gmail.com" },
                { "PhoneNumber", 976543210 },
                { "BirthDate", DateTime.Parse("1990-01-01") },
                { "Address", "123 Main Street, Anytown, USA" }
            };
            var customer = CreateEmpFromDictionary(customer1);
            
            var validationContext = new ValidationContext(customer) { MemberName = "BirthDate" };
            var minAgeAttribute = new MinAgeAttribute(18);

            // Act
            var validationResult = minAgeAttribute.GetValidationResult(customer.BirthDate, validationContext);

            // Assert
            Assert.IsNotNull(validationResult, "Validation should fail for invalid age.");
        }

        
    }
}
