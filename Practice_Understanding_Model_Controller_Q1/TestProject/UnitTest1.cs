using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using dotnetapp.Models;
using dotnetapp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Type _productControllerType;

        [SetUp]
        public void Setup()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _productControllerType = assembly.GetType("dotnetapp.Controllers.ProductController");
        }

        [Test]
        public void Test_Index_Action()
        {
            var indexMethod = _productControllerType.GetMethod("Index");
            var controllerInstance = Activator.CreateInstance(_productControllerType);

            var result = indexMethod.Invoke(controllerInstance, null) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_Create_Get_Action()
        {
            var createGetMethod = _productControllerType.GetMethod("Create", new Type[] { });
            var controllerInstance = Activator.CreateInstance(_productControllerType);

            var result = createGetMethod.Invoke(controllerInstance, null) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_Create_Post_Action()
        {
            var createPostMethod = _productControllerType.GetMethod("Create", new Type[] { typeof(Product) });
            var controllerInstance = Activator.CreateInstance(_productControllerType);
            var product = new Product();

            var result = createPostMethod.Invoke(controllerInstance, new object[] { product }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        
        [Test]
        public void Test_Edit_Post_Action_Exists()
        {
            // Arrange
            var editPostMethod = _productControllerType.GetMethod("Edit", new Type[] { typeof(Product) });
            var controllerInstance = Activator.CreateInstance(_productControllerType);
            var product = new Product(); // Assuming a Product object is required as parameter

            // Act
            var result = editPostMethod.Invoke(controllerInstance, new object[] { product }) as IActionResult;

            // Assert
            Assert.NotNull(editPostMethod);
        }



        [Test]
        public void Test_Delete_Get_Action()
        {
            var deleteGetMethod = _productControllerType.GetMethod("Delete", new Type[] { typeof(int) });
            var controllerInstance = Activator.CreateInstance(_productControllerType);

            var result = deleteGetMethod.Invoke(controllerInstance, new object[] { 1 }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_DeleteConfirmed_Post_Action()
        {
            var deleteConfirmedPostMethod = _productControllerType.GetMethod("DeleteConfirmed", new Type[] { typeof(int) });
            var controllerInstance = Activator.CreateInstance(_productControllerType);

            var result = deleteConfirmedPostMethod.Invoke(controllerInstance, new object[] { 1 }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName); // Ensure it redirects to Index action
        }

    }
}
