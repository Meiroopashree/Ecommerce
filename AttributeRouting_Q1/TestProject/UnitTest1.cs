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
    public class MenuControllerTests
    {

        private IHtmlHelper _htmlHelper;
        
        [Test]
        public void Test_Products_Route_Attribute()
        {
            // Arrange
            var controller = CreateMenuController();
            var method = GetActionMethod1(controller, "Products");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("menu/products", routeAttribute.Template);
        }

        [Test]
        public void Test_Customers_Route_Attribute()
        {
            // Arrange
            var controller = CreateMenuController();
            var method = GetActionMethod1(controller, "Customers");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("menu/customers", routeAttribute.Template);
        }

        [Test]
        public void Test_Home_Route_Attribute()
        {
            // Arrange
            var controller = CreateMenuController();
            var method = GetActionMethod1(controller, "Home");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("menu/home", routeAttribute.Template);
        }

        [Test]
        public void Test_Orders_Route_Attribute()
        {
            // Arrange
            var controller = CreateMenuController();
            var method = GetActionMethod1(controller, "Orders");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("menu/orders", routeAttribute.Template);
        }
        [Test]
        public void Test_HomeViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Menu/", "Home.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Home.cshtml view file does not exist.");
        }

        [Test]
        public void Test_ProductsViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Menu/", "Products.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Products.cshtml view file does not exist.");
        }

        [Test]
        public void Test_CustomersViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Menu/", "Customers.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Customers.cshtml view file does not exist.");
        }

        [Test]
        public void Test_OrdersViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Menu/", "Orders.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Orders.cshtml view file does not exist.");
        }
         private MethodInfo GetActionMethod1(MenuController controller, string methodName)
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

        private MenuController CreateMenuController()
        {
            // Fully-qualified name of the MenuController class
            string controllerTypeName = "dotnetapp.Controllers.MenuController, dotnetapp";

            // Get the type using Type.GetType
            Type controllerType = Type.GetType(controllerTypeName);

            // Check if the type is found
            Assert.IsNotNull(controllerType);

            // Create an instance of the controller using reflection
            return (MenuController)Activator.CreateInstance(controllerType);
        }

        private IActionResult GetActionMethod(MenuController controller, string methodName)
        {
            // Use reflection to get the method by name
            MethodInfo method = controller.GetType().GetMethod(methodName);

            if (method != null && method.ReturnType == typeof(IActionResult))
            {
                return (IActionResult)method.Invoke(controller, null);
            }
            else
            {
                // Handle the case where the method doesn't exist or doesn't return IActionResult
                throw new Exception("Action method not found or doesn't return IActionResult.");
            }
        }

        private void AssertIsViewResultWithCorrectViewName(IActionResult result, string expectedViewName)
        {
            Assert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(expectedViewName, viewResult.ViewName);
        }

    }
}