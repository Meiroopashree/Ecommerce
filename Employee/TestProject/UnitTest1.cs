using System.Numerics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Microsoft.Data.SqlClient;
using System;



namespace TestProject;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    // [Test]
    // public void Test1()
    // {
    //     Assert.Pass();
    // }

        [Test]
        public void Candidate_Models_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Candidate";
            Assembly assembly = Assembly.Load(assemblyName);
            Type CandidateType = assembly.GetType(typeName);
            Assert.IsNotNull(CandidateType);
        }

        [Test]
        public void VerificationTask_Models_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.VerificationTask";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationTaskType = assembly.GetType(typeName);
            Assert.IsNotNull(VerificationTaskType);
        }

        [Test]
        public void Candidate_CandidateID_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Candidate";
            Assembly assembly = Assembly.Load(assemblyName);
            Type CandidateType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = CandidateType.GetProperty("CandidateID");
            Assert.IsNotNull(propertyInfo, "Property CandidateID does not exist in Candidate class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property CandidateID in Candidate class is not of type int");
        }

        [Test]
        public void Candidate_CandidateName_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Candidate";
            Assembly assembly = Assembly.Load(assemblyName);
            Type CandidateType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = CandidateType.GetProperty("CandidateName");
            Assert.IsNotNull(propertyInfo, "Property CandidateName does not exist in Candidate class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property CandidateName in Candidate class is not of type string");
        }

        [Test]
        public void VerificationTask_TaskID_PropertyExists_ReturnExpectedDataTypes_int()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.VerificationTask";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationTaskType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = VerificationTaskType.GetProperty("TaskID");
            Assert.IsNotNull(propertyInfo, "Property TaskID does not exist in VerificationTask class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(int), expectedType, "Property TaskID in VerificationTask class is not of type int");
        }

        
        [Test]
        public void VerificationTask_VerificationDetails_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.VerificationTask";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationTaskType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = VerificationTaskType.GetProperty("VerificationDetails");
            Assert.IsNotNull(propertyInfo, "Property VerificationDetails does not exist in VerificationTask class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property VerificationDetails in VerificationTask class is not of type string");
        }

        [Test]
        public void VerificationTask_Status_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.VerificationTask";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationTaskType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = VerificationTaskType.GetProperty("Status");
            Assert.IsNotNull(propertyInfo, "Property Status does not exist in VerificationTask class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Status in VerificationTask class is not of type string");
        }

        [Test]
        public void CandidateController_Controllers_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.CandidateController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type CandidateControllerType = assembly.GetType(typeName);
            Assert.IsNotNull(CandidateControllerType);
        }

        [Test]
        public void VerificationController_Controllers_ClassExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.VerificationController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationControllerType = assembly.GetType(typeName);
            Assert.IsNotNull(VerificationControllerType);
        }


        [Test]
        public void VerificationController_Index_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.VerificationController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = VerificationControllerType.GetMethod("Index");
            Assert.IsNotNull(methodInfo, "Method Index does not exist in VerificationController class");
        }

        [Test]
        public void VerificationController_Delete_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.VerificationController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = VerificationControllerType.GetMethod("Delete");
            Assert.IsNotNull(methodInfo, "Method Delete does not exist in VerificationController class");
        }

        public void VerificationController_Edit_MethodExists()
        {
            string assemblyName = "dotnetapp"; 
            string typeName = "dotnetapp.Controllers.VerificationController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type verificationControllerType = assembly.GetType(typeName);
            MethodInfo editMethod = verificationControllerType.GetMethod("Edit");
            Assert.NotNull(editMethod, "Method Edit does not exist in VerificationController class");
        }

        [Test]
        public void VerificationController_Task_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.VerificationController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = VerificationControllerType.GetMethod("Task");
            Assert.IsNotNull(methodInfo, "Method Task does not exist in VerificationController class");
        }

        [Test]
        public void CandidateController_Create_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.CandidateController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type CandidateControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = CandidateControllerType.GetMethod("Create", Type.EmptyTypes);
            Assert.IsNotNull(methodInfo, "Method Create does not exist in CandidateController class");
        }

        [Test]
        public void VerificationController_Create_MethodExists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Controllers.VerificationController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = VerificationControllerType.GetMethod("Create", Type.EmptyTypes);
            Assert.IsNotNull(methodInfo, "Method Create does not exist in VerificationController class");
        }


        // [Test]
        // public void CandidateController_Create_Method_with_NoParams_Returns_ActionResult()
        // {
        //     string assemblyName = "dotnetapp";
        //     string typeName = "dotnetapp.Controllers.CandidateController";
        //     Assembly assembly = Assembly.Load(assemblyName);
        //     Type CandidateControllerType = assembly.GetType(typeName);
        //     MethodInfo methodInfo = CandidateControllerType.GetMethod("Create", Type.EmptyTypes);
        //     Assert.AreEqual(typeof(ActionResult), methodInfo.ReturnType,
        //         "Method Create in CandidateController class is not of type ActionResult");
        // }


        // [Test]
        // public void VerificationController_Delete_MethodReturns_ActionResult()
        // {
        //     string assemblyName = "dotnetapp";
        //     string typeName = "dotnetapp.Controllers.VerificationController";
        //     Assembly assembly = Assembly.Load(assemblyName);
        //     Type VerificationControllerType = assembly.GetType(typeName);
        //     MethodInfo methodInfo = VerificationControllerType.GetMethod("Delete");
        //     Assert.AreEqual(typeof(ActionResult), methodInfo.ReturnType, "Method Delete in VerificationController class is not of type ActionResult");
        // }

        [Test]
        public void VerificationController_Delete_MethodReturns_ActionResult()
        {
            string assemblyName = "dotnetapp"; // Replace with your assembly name
            string typeName = "dotnetapp.Controllers.VerificationController";
            Assembly assembly = Assembly.Load(assemblyName);
            Type VerificationControllerType = assembly.GetType(typeName);
            MethodInfo methodInfo = VerificationControllerType.GetMethod("Delete");
            Assert.IsNotNull(methodInfo, "Method Delete does not exist in VerificationController class");
            var returnType = methodInfo.ReturnType;
            Assert.IsTrue(typeof(ActionResult).IsAssignableFrom(returnType), $"Method Delete in VerificationController class is not of type ActionResult. It is {returnType}");
        }

}