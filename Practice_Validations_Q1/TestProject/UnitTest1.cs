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
        public void Customer_Properties_Have_RequiredAttribute()
        {
            var count = 0;

            Type customerType = typeof(Customer);
            PropertyInfo[] properties = customerType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Name")
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
        public void Customer_Properties_Have_RangeAttribute()
        {
            var count = 0;

            Type customerType = typeof(Customer);
            PropertyInfo[] properties = customerType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Salary")
                {
                    var rangeAttribute = property.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
                    Assert.NotNull(rangeAttribute, $"{property.Name} should have a RangeAttribute.");
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
        public void Customer_Properties_Have_DataTypeAttribute()
        {
            var count = 0;
            Type customerType = typeof(Customer);
            PropertyInfo[] properties = customerType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Dob")
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
        public void Customer_Property_Name_Validation()
        {
            var customer1 = new Dictionary<string, object>
            {
                { "Name", "" },
                { "Email", "a@gmail.com" },
                { "Salary", 1500 },
                { "Dob", DateTime.Parse("1990-01-01") },
                { "Dept", "HR" }
            };
            var customer = CreateEmpFromDictionary(customer1);
            string expectedErrorMessage = "The Name field is required.";
            var context = new ValidationContext(customer, null, null);
            var results = new List<ValidationResult>();

            var validationContext = new ValidationContext(customer) { MemberName = "Name" };
            var requiredAttribute = new RequiredAttribute();

            // Act
            var validationResult = requiredAttribute.GetValidationResult(customer.Name, validationContext);

            // Assert
            Assert.IsNotNull(validationResult, "Validation should fail for null Name.");
            Assert.AreEqual("The Name field is required.", validationResult.ErrorMessage, "Error message should match.");


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
                { "Name", "asd" },
                { "Email", "" },
                { "Salary", 1500 },
                { "Dob", DateTime.Parse("1990-01-01") },
                { "Dept", "HR" }
            };
            var customer = CreateEmpFromDictionary(customer1);
            string expectedErrorMessage = "The Email field is required.";
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

            PropertyInfo propertyInfo = idType.GetProperty("Id");

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

            PropertyInfo propertyInfo = emailType.GetProperty("Name");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        [Test]
        public void CustomerClass_HasSalaryProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Customer";

            Assembly assembly = Assembly.Load(assemblyName);
            Type salaryType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = salaryType.GetProperty("Salary");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(decimal), propertyInfo.PropertyType);
        }

        [Test]
        public void CustomerClass_HasDobProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Customer";

            Assembly assembly = Assembly.Load(assemblyName);
            Type customerType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = customerType.GetProperty("Dob");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DateTime), propertyInfo.PropertyType);
        }

        // Test case for Dept property
        [Test]
        public void CustomerClass_HasDeptProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Customer";

            Assembly assembly = Assembly.Load(assemblyName);
            Type customerType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = customerType.GetProperty("Dept");

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
            PropertyInfo dobProperty = customerType.GetProperty("Dob");

            // Act
            var minAgeAttribute = dobProperty.GetCustomAttribute<MinAgeAttribute>();

            // Assert
            Assert.IsNotNull(minAgeAttribute, "MinAge attribute should be applied to the Dob property");
        }

        [Test]
        public void MinAgeAttribute_ValidAge_ShouldPass()
        {
            var customer1 = new Dictionary<string, object>
            {
                { "Name", "asd" },
                { "Email", "" },
                { "Salary", 1500 },
                { "Dob", DateTime.Now.AddYears(-30) },
                { "Dept", "HR" }
            };
            var customer = CreateEmpFromDictionary(customer1);
            var validationContext = new ValidationContext(customer) { MemberName = "Dob" };
            var minAgeAttribute = new MinAgeAttribute(25);

            // Act
            var validationResult = minAgeAttribute.GetValidationResult(employee.Dob, validationContext);
            // Assert
            Assert.IsNull(validationResult, "Validation should pass for valid age.");
        }

         [Test]
        public void MinAgeAttribute_InvalidAge_ShouldFail()
        {
            var employee1 = new Dictionary<string, object>
            {
                { "Name", "asd" },
                { "Email", "" },
                { "Salary", 1500 },
                { "Dob", DateTime.Now.AddYears(-20) },
                { "Dept", "HR" }
            };
            var employee = CreateEmpFromDictionary(employee1);
            // var employee = new Employee { Dob = DateTime.Now.AddYears(-20) }; // Assuming employee is 20 years old
            var validationContext = new ValidationContext(employee) { MemberName = "Dob" };
            var minAgeAttribute = new MinAgeAttribute(25);

            // Act
            var validationResult = minAgeAttribute.GetValidationResult(employee.Dob, validationContext);

            // Assert
            Assert.IsNotNull(validationResult, "Validation should fail for invalid age.");
        }

        
    }
}
