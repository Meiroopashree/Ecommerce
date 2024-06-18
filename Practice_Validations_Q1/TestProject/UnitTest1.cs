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
    public class EmployeeTests
    {
        [Test]
        public void Employee_Properties_Have_RequiredAttribute()
        {
            var count = 0;

            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

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
        public void Employee_Properties_Have_EmailAddressAttribute()
        {
            var count = 0;
            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

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
        public void Employee_Properties_Have_RangeAttribute()
        {
            var count = 0;

            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

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
        public void Employee_Properties_Have_DataTypeAttribute()
        {
            var count = 0;
            Type employeeType = typeof(Employee);
            PropertyInfo[] properties = employeeType.GetProperties();

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

        //[Test]
        //public void Employee_Properties_Have_MinAgeAttribute()
        //{
        //    var count = 0;
        //    Type employeeType = typeof(Employee);
        //    PropertyInfo[] properties = employeeType.GetProperties();

        //    foreach (var property in properties)
        //    {
        //        if (property.Name == "Dob")
        //        {
        //            var minAgeAttribute = property.GetCustomAttribute<MinAgeAttribute>();
        //            Assert.NotNull(minAgeAttribute, $"{property.Name} should have a MinAgeAttribute.");
        //            count++;
        //            break;
        //        }
        //    }
        //    if( count == 0)
        //    { Assert.Fail(); }
        //}

        //[TestCase("Alice Brown", "alice@example.com", 1500, "1990-01-01", "HR", null)] // Valid case, no error expected
        [Test]
        public void Employee_Property_Name_Validation()
        {
            var employee1 = new Dictionary<string, object>
            {
                { "Name", "" },
                { "Email", "a@gmail.com" },
                { "Salary", 1500 },
                { "Dob", DateTime.Parse("1990-01-01") },
                { "Dept", "HR" }
            };
            var employee = CreateEmpFromDictionary(employee1);
            string expectedErrorMessage = "The Name field is required.";
            var context = new ValidationContext(employee, null, null);
            var results = new List<ValidationResult>();

            var validationContext = new ValidationContext(employee) { MemberName = "Name" };
            var requiredAttribute = new RequiredAttribute();

            // Act
            var validationResult = requiredAttribute.GetValidationResult(employee.Name, validationContext);

            // Assert
            Assert.IsNotNull(validationResult, "Validation should fail for null Name.");
            Assert.AreEqual("The Name field is required.", validationResult.ErrorMessage, "Error message should match.");


            bool isValid = Validator.TryValidateObject(employee, context, results);

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

        public Employee CreateEmpFromDictionary(Dictionary<string, object> data)
        {
            var player = new Employee();
            foreach (var kvp in data)
            {
                var propertyInfo = typeof(Employee).GetProperty(kvp.Key);
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
        public void Employee_Property_Email_Validation()
        {
           
            var employee1 = new Dictionary<string, object>
            {
                { "Name", "asd" },
                { "Email", "" },
                { "Salary", 1500 },
                { "Dob", DateTime.Parse("1990-01-01") },
                { "Dept", "HR" }
            };
            var employee = CreateEmpFromDictionary(employee1);
            string expectedErrorMessage = "The Email field is required.";
            var context = new ValidationContext(employee, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(employee, context, results);

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

        //Checking if EmployeeController exists
        [Test]
        public void EmployeeControllerExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.EmployeeController";

            Assembly assembly = Assembly.Load(assemblyName);
            Type EmpControllerType = assembly.GetType(typeName);

            Assert.IsNotNull(EmpControllerType, "EmployeeController does not exist in the assembly.");
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
        public void EmployeeClass_HasIDProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Employee";

            Assembly assembly = Assembly.Load(assemblyName);
            Type idType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = idType.GetProperty("Id");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(int), propertyInfo.PropertyType);
        }

        //Test if Email property is present
        [Test]
        public void EmployeeClass_HasEmailProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Employee";

            Assembly assembly = Assembly.Load(assemblyName);
            Type emailType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = emailType.GetProperty("Name");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        [Test]
        public void EmployeeClass_HasSalaryProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Employee";

            Assembly assembly = Assembly.Load(assemblyName);
            Type salaryType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = salaryType.GetProperty("Salary");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(decimal), propertyInfo.PropertyType);
        }

        [Test]
        public void EmployeeClass_HasDobProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Employee";

            Assembly assembly = Assembly.Load(assemblyName);
            Type employeeType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = employeeType.GetProperty("Dob");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DateTime), propertyInfo.PropertyType);
        }

        // Test case for Dept property
        [Test]
        public void EmployeeClass_HasDeptProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Employee";

            Assembly assembly = Assembly.Load(assemblyName);
            Type employeeType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = employeeType.GetProperty("Dept");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        //[Test]
        //public void EmployeeController_CreateMethodExists()
        //{
        //    string assemblyName = "dotnetapp"; // Update with your correct assembly name
        //    string typeName = "dotnetapp.Controllers.EmployeeController";
        //    Assembly assembly = Assembly.Load(assemblyName);
        //    Type controllerType = assembly.GetType(typeName);

        //    // Specify the parameter types for the search method
        //    Type[] parameterTypes = new Type[] { typeof(string) }; // Adjust this based on your method signature

        //    // Find the Search method with the specified parameter types
        //    MethodInfo createMethod = controllerType.GetMethod("Se", parameterTypes);
        //    Assert.IsNotNull(createMethod);
        //}
        [Test]
        public void EmployeeController_CreateMethodExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.EmployeeController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Specify the parameter types for the search method
            Type[] parameterTypes = new Type[] { typeof(Employee) }; // Adjust based on your method signature

            // Find the Create method with the specified parameter types
            MethodInfo createMethod = controllerType.GetMethod("Create", parameterTypes);
            Assert.IsNotNull(createMethod);
        }

        [Test]
        public void EmployeeController_SuccessMethodExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.EmployeeController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Find the Success method without parameters
            MethodInfo successMethod = controllerType.GetMethod("Success", Type.EmptyTypes);
            Assert.IsNotNull(successMethod);
        }

        [Test]
        public void EmployeeController_Constructor_InjectsDbContext()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.EmployeeController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            var constructor = controllerType.GetConstructors().FirstOrDefault(); // Get the constructor
            var parameters = constructor?.GetParameters().FirstOrDefault(); // Check if it expects parameters
            Assert.IsNotNull(parameters);
            Assert.AreEqual(typeof(ApplicationDbContext), parameters.ParameterType); // Ensure ApplicationDbContext is injected
        }

        //[Test]
        //public void EmployeeController_CreateAction_HasHttpPostAttribute()
        //{
        //    string assemblyName = "dotnetapp"; // Your project assembly name
        //    string typeName = "dotnetapp.Controllers.EmployeeController";
        //    Assembly assembly = Assembly.Load(assemblyName);
        //    Type controllerType = assembly.GetType(typeName);

        //    var createMethod = controllerType.GetMethod("Create", new[] { typeof(Employee) });
        //    var httpPostAttribute = createMethod?.GetCustomAttributes(typeof(HttpPostAttribute), false);
        //    Assert.IsNotNull(httpPostAttribute);
        //}

        //[Test]
        //public void Test_IndexViewFile_Exists()
        //{
        //    string indexPath = Path.Combine(@"/home/coder/project/workspace/Furniture-MVC-ADO/dotnetapp/Views/Furniture", "Index.cshtml");
        //    bool indexViewExists = File.Exists(indexPath);

        //    Assert.IsTrue(indexViewExists, "Index.cshtml view file does not exist.");
        //}

        [Test]
        public void Test_CreateViewFile_Exists()
        {
            string createPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Employee/Create.cshtml");
            bool createViewExists = File.Exists(createPath);

            Assert.IsTrue(createViewExists, "Create.cshtml view file does not exist.");
        }
        [Test]
        public void Test_SuccessViewFile_Exists()
        {
            string successPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Employee/Success.cshtml");
            bool successViewExists = File.Exists(successPath);

            Assert.IsTrue(successViewExists, "Success.cshtml view file does not exist.");
        }
//         [Test]
// public void Employee_Property_MinAge_Validation()
// {
//     var employeeData = new Dictionary<string, object>
//     {
//         { "Name", "John Doe" },
//         { "Email", "john@example.com" },
//         { "Salary", 1500 },
//         { "Dob", DateTime.Now.AddYears(-25).AddDays(1) }, // Adjusted to ensure below minimum age
//         { "Dept", "HR" }
//     };

//     var employee = CreateEmployeeFromDictionary(employeeData);
//     var context = new ValidationContext(employee, null, null);
//     var results = new List<ValidationResult>();

//     bool isValid = Validator.TryValidateObject(employee, context, results);

//     if (isValid)
//     {
//         Assert.Fail("Validation should have failed due to age requirement.");
//     }
//     else
//     {
//         // Validation should fail, checking if the error message matches expected
//         string expectedErrorMessage = "Employee must be 25 years or older";
//         var actualErrorMessages = results.Select(r => r.ErrorMessage).ToList();
        
//         Console.WriteLine("Actual Error Messages:");
//         foreach (var errorMessage in actualErrorMessages)
//         {
//             Console.WriteLine(errorMessage);
//         }

//         CollectionAssert.Contains(actualErrorMessages, expectedErrorMessage);
//     }
// }
         private Employee CreateEmployeeFromDictionary(Dictionary<string, object> data)
        {
            var employee = new Employee();
            foreach (var kvp in data)
            {
                var propertyInfo = typeof(Employee).GetProperty(kvp.Key);
                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType == typeof(decimal) && kvp.Value is int intValue)
                    {
                        propertyInfo.SetValue(employee, (decimal)intValue);
                    }
                    else
                    {
                        propertyInfo.SetValue(employee, kvp.Value);
                    }
                }
            }
            return employee;
        }
        [Test]
        public void Employee_Properties_Have_UniqueEmailAttribute()
        {
            Type employeeType = typeof(Employee);
            PropertyInfo emailProperty = employeeType.GetProperty("Email");

            var uniqueEmailAttribute = emailProperty.GetCustomAttribute<UniqueEmailAttribute>();

            Assert.IsNotNull(uniqueEmailAttribute, "UniqueEmail attribute should be applied to the Email property");
        }

        [Test]
        public void Employee_Properties_Have_MinAgeAttribute()
        {
            // Arrange
            Type employeeType = typeof(Employee);
            PropertyInfo dobProperty = employeeType.GetProperty("Dob");

            // Act
            var minAgeAttribute = dobProperty.GetCustomAttribute<MinAgeAttribute>();

            // Assert
            Assert.IsNotNull(minAgeAttribute, "MinAge attribute should be applied to the Dob property");
        }

        [Test]
        public void MinAgeAttribute_ValidAge_ShouldPass()
        {
            var employee1 = new Dictionary<string, object>
            {
                { "Name", "asd" },
                { "Email", "" },
                { "Salary", 1500 },
                { "Dob", DateTime.Now.AddYears(-30) },
                { "Dept", "HR" }
            };
            var employee = CreateEmpFromDictionary(employee1);
            var validationContext = new ValidationContext(employee) { MemberName = "Dob" };
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
