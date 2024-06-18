using dotnetapp.Controllers;
using RazorLight;
using Microsoft.AspNetCore.Html;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Text.Encodings.Web;
using System.Xml.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Hosting.Server;
using dotnetapp;
using System.Net;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.WebEncoders.Testing;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        [Test]
        public void Test_Reviews_Route_Attribute()
        {
            // Arrange
            var controller = CreateProductController();
            var method = GetActionMethod(controller, "Reviews");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("product/reviews", routeAttribute.Template);
        }

        [Test]
        public void Test_List_Route_Attribute()
        {
            // Arrange
            var controller = CreateProductController();
            var method = GetActionMethod(controller, "List");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("product/list", routeAttribute.Template);
        }

        [Test]
        public void Test_Info_Route_Attribute()
        {
            // Arrange
            var controller = CreateProductController();
            var method = GetActionMethod(controller, "Info");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("product/info", routeAttribute.Template);
        }

        [Test]
        public void Test_Category_Route_Attribute()
        {
            // Arrange
            var controller = CreateProductController();
            var method = GetActionMethod(controller, "Category");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("product/category", routeAttribute.Template);
        }

        [Test]
        public void Test_ReviewsViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Product/", "Reviews.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Reviews.cshtml view file does not exist.");
        }

        [Test]
        public void Test_ListViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Product/", "List.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "List.cshtml view file does not exist.");
        }

        [Test]
        public void Test_InfoViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Product/", "Info.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Info.cshtml view file does not exist.");
        }

        [Test]
        public void Test_CategoryViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Product/", "Category.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Category.cshtml view file does not exist.");
        }
        

        private MethodInfo GetActionMethod(ProductController controller, string methodName)
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

        private ProductController CreateProductController()
        {
            // Fully-qualified name of the ProductController class
            string controllerTypeName = "dotnetapp.Controllers.ProductController, dotnetapp";

            // Get the type using Type.GetType
            Type controllerType = Type.GetType(controllerTypeName);

            // Check if the type is found
            Assert.IsNotNull(controllerType);

            // Create an instance of the controller using reflection
            return (ProductController)Activator.CreateInstance(controllerType);
        }
    }
}
