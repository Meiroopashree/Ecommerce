using dotnetapp.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class BookControllerTests
    {
        [Test]
        public void Test_Home_Route_Attribute()
        {
            // Arrange
            var controller = CreateBookController();
            var method = GetActionMethod(controller, "Home");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("book/home", routeAttribute.Template);
        }

        [Test]
        public void Test_Books_Route_Attribute()
        {
            // Arrange
            var controller = CreateBookController();
            var method = GetActionMethod(controller, "Books");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("book/books", routeAttribute.Template);
        }

        [Test]
        public void Test_Authors_Route_Attribute()
        {
            // Arrange
            var controller = CreateBookController();
            var method = GetActionMethod(controller, "Authors");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("book/authors", routeAttribute.Template);
        }

        [Test]
        public void Test_Categories_Route_Attribute()
        {
            // Arrange
            var controller = CreateBookController();
            var method = GetActionMethod(controller, "Categories");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("book/categories", routeAttribute.Template);
        }

        [Test]
        public void Test_HomeViewFile_Exists()
        {
            string homePath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Book/", "Home.cshtml");
            bool homeViewExists = File.Exists(homePath);

            Assert.IsTrue(homeViewExists, "Home.cshtml view file does not exist.");
        }

        [Test]
        public void Test_BooksViewFile_Exists()
        {
            string booksPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Book/", "Books.cshtml");
            bool booksViewExists = File.Exists(booksPath);

            Assert.IsTrue(booksViewExists, "Books.cshtml view file does not exist.");
        }

        [Test]
        public void Test_AuthorsViewFile_Exists()
        {
            string authorsPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Book/", "Authors.cshtml");
            bool authorsViewExists = File.Exists(authorsPath);

            Assert.IsTrue(authorsViewExists, "Authors.cshtml view file does not exist.");
        }

        [Test]        
        public void Test_CategoriesViewFile_Exists()
        {
            string categoriesPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Book/", "Categories.cshtml");
            bool categoriesViewExists = File.Exists(categoriesPath);

            Assert.IsTrue(categoriesViewExists, "Categories.cshtml view file does not exist.");
        }

        private MethodInfo GetActionMethod(BookController controller, string methodName)
        {
            // Use reflection to get the method by name
            MethodInfo method = controller.GetType().GetMethod(methodName);

            if (method != null && method.ReturnType == typeof(IActionResult))
            {
                return method;
            }
            else
            {
                // Handle the case where the method doesn't exist or doesn't return IActionResult
                throw new Exception("Action method not found or doesn't return IActionResult.");
            }
        }

        private BookController CreateBookController()
        {
            // Fully-qualified name of the BookController class
            string controllerTypeName = "dotnetapp.Controllers.BookController, dotnetapp";

            // Get the type using Type.GetType
            Type controllerType = Type.GetType(controllerTypeName);

            // Check if the type is found
            Assert.IsNotNull(controllerType);

            // Create an instance of the controller using reflection
            return (BookController)Activator.CreateInstance(controllerType);
        }


        private void AssertIsViewResultWithCorrectViewName(IActionResult result, string expectedViewName)
        {
            Assert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(expectedViewName, viewResult.ViewName);
        }
    }
}
