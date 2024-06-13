using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using dotnetapp.Controllers;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class BlogControllerTests
    {
        [Test]
        public void Test_Home_Route_Attribute()
        {
            // Arrange
            var controller = CreateBlogController();
            var method = GetActionMethod(controller, "Home");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("blog/home", routeAttribute.Template);
        }

        [Test]
        public void Test_Posts_Route_Attribute()
        {
            // Arrange
            var controller = CreateBlogController();
            var method = GetActionMethod(controller, "Posts");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("blog/posts", routeAttribute.Template);
        }

        [Test]
        public void Test_Authors_Route_Attribute()
        {
            // Arrange
            var controller = CreateBlogController();
            var method = GetActionMethod(controller, "Authors");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("blog/authors", routeAttribute.Template);
        }


        [Test]
        public void Test_Categories_Route_Attribute()
        {
            // Arrange
            var controller = CreateBlogController();
            var method = GetActionMethod(controller, "Categories");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("blog/categories", routeAttribute.Template);
        }

        [Test]
        public void Test_HomeViewFile_Exists()
        {
            string homePath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Blog/", "Home.cshtml");
            bool homeViewExists = File.Exists(homePath);

            Assert.IsTrue(homeViewExists, "Home.cshtml view file does not exist.");
        }

        [Test]
        public void Test_PostsViewFile_Exists()
        {
            string postsPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Blog/", "Posts.cshtml");
            bool postsViewExists = File.Exists(postsPath);

            Assert.IsTrue(postsViewExists, "Posts.cshtml view file does not exist.");
        }

        [Test]
        public void Test_AuthorsViewFile_Exists()
        {
            string authorsPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Blog/", "Authors.cshtml");
            bool authorsViewExists = File.Exists(authorsPath);

            Assert.IsTrue(authorsViewExists, "Authors.cshtml view file does not exist.");
        }

        [Test]
        public void Test_CategoriesViewFile_Exists()
        {
            string categoriesPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Blog/", "Categories.cshtml");
            bool categoriesViewExists = File.Exists(categoriesPath);

            Assert.IsTrue(categoriesViewExists, "Categories.cshtml view file does not exist.");
        }

        private MethodInfo GetActionMethod(BlogController controller, string methodName)
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

        private BlogController CreateBlogController()
        {
            // Fully-qualified name of the BlogController class
            string controllerTypeName = "dotnetapp.Controllers.BlogController, dotnetapp";

            // Get the type using Type.GetType
            Type controllerType = Type.GetType(controllerTypeName);

            // Check if the type is found
            Assert.IsNotNull(controllerType);

            // Create an instance of the controller using reflection
            return (BlogController)Activator.CreateInstance(controllerType);
        }
    }
}
