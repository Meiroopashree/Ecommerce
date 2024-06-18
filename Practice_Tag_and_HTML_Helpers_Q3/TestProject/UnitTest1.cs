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
    public class ServiceControllerTests
    {
        [Test]
        public void Test_Details_Route_Attribute()
        {
            // Arrange
            var controller = CreateServiceController();
            var method = GetActionMethod(controller, "Details");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("service/details", routeAttribute.Template);
        }

        [Test]
        public void Test_Overview_Route_Attribute()
        {
            // Arrange
            var controller = CreateServiceController();
            var method = GetActionMethod(controller, "Overview");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("product/overview", routeAttribute.Template);
        }

        [Test]
        public void Test_Pricing_Route_Attribute()
        {
            // Arrange
            var controller = CreateProductController();
            var method = GetActionMethod(controller, "Pricing");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("product/pricing", routeAttribute.Template);
        }

        [Test]
        public void Test_Testimonials_Route_Attribute()
        {
            // Arrange
            var controller = CreateProductController();
            var method = GetActionMethod(controller, "Testimonials");

            // Act
            var routeAttribute = method.GetCustomAttribute<RouteAttribute>();

            // Assert
            Assert.IsNotNull(routeAttribute);
            Assert.AreEqual("product/testimonials", routeAttribute.Template);
        }

        [Test]
        public void Test_DetailsViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Product/", "Details.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Details.cshtml view file does not exist.");
        }

        [Test]
        public void Test_OverviewViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Product/", "Overview.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Overview.cshtml view file does not exist.");
        }

        [Test]
        public void Test_PricingViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Product/", "Pricing.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Pricing.cshtml view file does not exist.");
        }

        [Test]
        public void Test_TestimonialsViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/dotnetapp/Views/Product/", "Testimonials.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Testimonials.cshtml view file does not exist.");
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
