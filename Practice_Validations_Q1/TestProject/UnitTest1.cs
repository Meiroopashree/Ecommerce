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
    public class BookTests
    {
        [Test]
        public void Book_Author_Have_RequiredAttribute()
        {
            var count = 0;

            Type BookType = typeof(Book);
            PropertyInfo[] properties = BookType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Author")
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
        public void Book_Properties_Have_UniqueTitleAttribute()
        {
            var count = 0;
            Type BookType = typeof(Book);
            PropertyInfo[] properties = BookType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Title")
                {
                    var titleAttribute = property.GetCustomAttribute<UniqueTitleAttribute>();
                    Assert.NotNull(titleAttribute, $"{property.Name} should have an UniqueTitleAttribute.");
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
        public void Book_Properties_Have_DataTypeAttribute()
        {
            var count = 0;
            Type BookType = typeof(Book);
            PropertyInfo[] properties = BookType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "PublishedDate")
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
        public void Book_Property_Title_Validation()
        {
            var bookData = new Dictionary<string, object>
            {
                { "Title", "" }, // Empty title to trigger validation error
                { "Author", "J.K. Rowling" },
                { "Genre", "Fantasy" },
                { "PublishedDate", DateTime.Parse("2023-01-01") }, // Future date to trigger PastDate validation error
                { "Price", 19.99 } // Valid price
            };

            var book = CreateBookFromDictionary(bookData);
            string expectedErrorMessage = "Title is required"; // Expected error message for Title validation

            var context = new ValidationContext(book, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(book, context, results, true);

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

        // Helper method to create a Book object from dictionary data
        private Book CreateBookFromDictionary(Dictionary<string, object> data)
        {
            var book = new Book();

            foreach (var entry in data)
            {
                PropertyInfo prop = book.GetType().GetProperty(entry.Key);

                if (prop != null)
                {
                    if (prop.PropertyType == typeof(decimal) && entry.Value is double doubleValue)
                    {
                        prop.SetValue(book, (decimal)doubleValue);
                    }
                    else
                    {
                        prop.SetValue(book, entry.Value);
                    }
                }
            }

            return book;
        }




        [Test]
        public void Book_Property_Author_Validation()
        {
            var Book1 = new Dictionary<string, object>
            {
                { "Title", "Harry Potter and the Sorcerer's Stone" }, // Empty title to trigger validation error
                { "Author", "" },
                { "Genre", "Fantasy" },
                { "PublishedDate", DateTime.Parse("2023-01-01") }, // Future date to trigger PastDate validation error
                { "Price", 19.99 } // Valid price
            };
            var Book = CreateBookFromDictionary(Book1);
            string expectedErrorMessage = "Author is required";
            var context = new ValidationContext(Book, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(Book, context, results, true);

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
        public void Book_Property_Genre_Validation()
        {
           
            var Book1 = new Dictionary<string, object>
            {
                { "Title", "Harry Potter and the Sorcerer's Stone" }, // Empty title to trigger validation error
                { "Author", "J.K. Rowling" },
                { "Genre", "" },
                { "PublishedDate", DateTime.Parse("2023-01-01") }, // Future date to trigger PastDate validation error
                { "Price", 19.99 } // Valid price
            };
            var Book = CreateBookFromDictionary(Book1);
            string expectedErrorMessage = "Genre is required";
            var context = new ValidationContext(Book, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(Book, context, results);

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
        public void Book_Property_PublishedDate_Validation()
        {
           
            var Book1 = new Dictionary<string, object>
            {
                { "Title", "Harry Potter and the Sorcerer's Stone" }, // Empty title to trigger validation error
                { "Author", "J.K. Rowling" },
                { "Genre", "Fantasy" },
                { "PublishedDate", DateTime.MinValue }, // Future date to trigger PastDate validation error
                { "Price", 19.99 } // Valid price
            };
            var Book = CreateBookFromDictionary(Book1);
            string expectedErrorMessage = "PublishedDate is required";
            var context = new ValidationContext(Book, null, null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(Book, context, results);

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

        //Checking if BookController exists
        [Test]
        public void BookControllerExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.BookController";

            Assembly assembly = Assembly.Load(assemblyName);
            Type EmpControllerType = assembly.GetType(typeName);

            Assert.IsNotNull(EmpControllerType, "BookController does not exist in the assembly.");
        }

        //Checking if PastDateAttribute class exists
        [Test]
        public void PastDateAttributeModelExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.PastDateAttribute";
            Assembly assembly = Assembly.Load(assemblyName);
            Type uniquetitleType = assembly.GetType(typeName);
            Assert.IsNotNull(uniquetitleType);
            var unique = Activator.CreateInstance(uniquetitleType);
            Assert.IsNotNull(unique);
        }

        [Test]
        public void BookClass_HasIDProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";

            Assembly assembly = Assembly.Load(assemblyName);
            Type idType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = idType.GetProperty("BookId");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(int), propertyInfo.PropertyType);
        }

        //Test if Title property is present
        [Test]
        public void BookClass_HasTitleProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";

            Assembly assembly = Assembly.Load(assemblyName);
            Type titleType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = titleType.GetProperty("Title");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        [Test]
        public void BookClass_HasAuthorProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";

            Assembly assembly = Assembly.Load(assemblyName);
            Type authorType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = authorType.GetProperty("Author");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        [Test]
        public void BookClass_HasGenreProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";

            Assembly assembly = Assembly.Load(assemblyName);
            Type genreType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = genreType.GetProperty("Genre");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(string), propertyInfo.PropertyType);
        }

        [Test]
        public void BookClass_HasPublishedDateProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";

            Assembly assembly = Assembly.Load(assemblyName);
            Type publishedType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = publishedType.GetProperty("PublishedDate");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(DateTime), propertyInfo.PropertyType);
        }

        // Test case for Address property
        [Test]
        public void BookClass_HasPriceProperty()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";

            Assembly assembly = Assembly.Load(assemblyName);
            Type BookType = assembly.GetType(typeName);

            PropertyInfo propertyInfo = BookType.GetProperty("Price");

            Assert.IsNotNull(propertyInfo);
            Assert.AreEqual(typeof(decimal), propertyInfo.PropertyType);
        }

        [Test]
        public void BookController_CreateMethodExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.BookController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Specify the parameter types for the search method
            Type[] parameterTypes = new Type[] { typeof(Book) }; // Adjust based on your method signature

            // Find the Create method with the specified parameter types
            MethodInfo createMethod = controllerType.GetMethod("Create", parameterTypes);
            Assert.IsNotNull(createMethod);
        }

        [Test]
        public void BookController_SuccessMethodExists()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.BookController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type controllerType = assembly.GetType(typeName);

            // Find the Success method without parameters
            MethodInfo successMethod = controllerType.GetMethod("Success", Type.EmptyTypes);
            Assert.IsNotNull(successMethod);
        }

        [Test]
        public void BookController_Constructor_InjectsDbContext()
        {
            string assemblyName = "dotnetapp"; // Your project assembly name
            string typeName = "dotnetapp.Controllers.BookController";
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
            string createPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Book/Create.cshtml");
            bool createViewExists = File.Exists(createPath);

            Assert.IsTrue(createViewExists, "Create.cshtml view file does not exist.");
        }
        [Test]
        public void Test_SuccessViewFile_Exists()
        {
            string successPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Book/Success.cshtml");
            bool successViewExists = File.Exists(successPath);

            Assert.IsTrue(successViewExists, "Success.cshtml view file does not exist.");
        }
    }
}
