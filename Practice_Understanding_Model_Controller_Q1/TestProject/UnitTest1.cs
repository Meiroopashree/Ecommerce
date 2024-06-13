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
    public class FeedbackControllerTests
    {
        private Type _feedbackControllerType;

        [SetUp]
        public void Setup()
        {
            Assembly assembly = Assembly.Load("dotnetapp");
            _feedbackControllerType = assembly.GetType("dotnetapp.Controllers.FeedbackController");
        }

        [Test]
        public void Test_Index_Action()
        {
            var indexMethod = _feedbackControllerType.GetMethod("Index");
            var controllerInstance = Activator.CreateInstance(_feedbackControllerType);

            var result = indexMethod.Invoke(controllerInstance, null) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_Create_Get_Action()
        {
            var createGetMethod = _feedbackControllerType.GetMethod("Create", new Type[] { });
            var controllerInstance = Activator.CreateInstance(_feedbackControllerType);

            var result = createGetMethod.Invoke(controllerInstance, null) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_Create_Post_Action()
        {
            var createPostMethod = _feedbackControllerType.GetMethod("Create", new Type[] { typeof(Feedback) });
            var controllerInstance = Activator.CreateInstance(_feedbackControllerType);
            var feedback = new Feedback();

            var result = createPostMethod.Invoke(controllerInstance, new object[] { feedback }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        
        [Test]
        public void Test_Edit_Post_Action_Exists()
        {
            // Arrange
            var editPostMethod = _feedbackControllerType.GetMethod("Edit", new Type[] { typeof(Feedback) });
            var controllerInstance = Activator.CreateInstance(_feedbackControllerType);
            var feedback = new Feedback(); // Assuming a Feedback object is required as parameter

            // Act
            var result = editPostMethod.Invoke(controllerInstance, new object[] { feedback }) as IActionResult;

            // Assert
            Assert.NotNull(editPostMethod);
        }



        [Test]
        public void Test_Delete_Get_Action()
        {
            var deleteGetMethod = _feedbackControllerType.GetMethod("Delete", new Type[] { typeof(int) });
            var controllerInstance = Activator.CreateInstance(_feedbackControllerType);

            var result = deleteGetMethod.Invoke(controllerInstance, new object[] { 1 }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Test_DeleteConfirmed_Post_Action()
        {
            var deleteConfirmedPostMethod = _feedbackControllerType.GetMethod("DeleteConfirmed", new Type[] { typeof(int) });
            var controllerInstance = Activator.CreateInstance(_feedbackControllerType);

            var result = deleteConfirmedPostMethod.Invoke(controllerInstance, new object[] { 1 }) as IActionResult;

            Assert.NotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.AreEqual("Index", (result as RedirectToActionResult).ActionName); // Ensure it redirects to Index action
        }

    }
}
