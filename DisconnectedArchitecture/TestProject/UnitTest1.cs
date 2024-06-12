using NUnit.Framework;
using System;
using System.IO;
using System.Data.SqlClient;
using DisconnectedArchitecture;
//using MusicalInstrumentsCRUD.Services;
using DisconnectedArchitecture.Models;
using System.Reflection;
using DisconnectArchitecture;
using System.Diagnostics.Metrics;

namespace DisconnectedArchitecture.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        private Type programType = typeof(Program);

        private Assembly assembly;
        private Assembly assembly1;
        //private InstrumentService instrumentService;

        private string connectionString;
        private string databaseName;
        private string tableName;
        private Type? _instrumentType;
        private int lastInsertedEmployeeId; // Store the ID of the last inserted product

        [OneTimeSetUp]
        public void LoadAssembly()
        {
            string assemblyPath = "DisconnectedArchitecture.dll"; // Adjust the path if needed
            assembly = Assembly.LoadFrom(assemblyPath);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Load the assembly containing the Program class (DisconnectArchitecture assembly).
            assembly1 = Assembly.LoadFrom("DisconnectedArchitecture.dll");
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            // Set up the connection string and other variables
            connectionString = ConnectionStringProvider.ConnectionString;
            databaseName = "appdb";
            tableName = "Employees";

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            DeleteEmployee(lastInsertedEmployeeId);
        }

        [Test]
        public void ConnectToDatabase_ConnectionSuccessful()
        {
            bool connectionSuccess = false;
            string errorMessage = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connectionSuccess = true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            Assert.IsTrue(connectionSuccess, "Failed to connect to the database. Error message: " + errorMessage);
        }

        [Test]
        public void Test_EmployeeClassExists()
        {
            string className = "DisconnectedArchitecture.Models.Employee";
            Type employeeType = assembly.GetType(className);
            Assert.NotNull(employeeType, $"The class '{className}' does not exist in the assembly.");
        }

        [Test]
        public void Test_EmployeeIDPropertyDataType_Int()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("EmployeeID");
            Assert.AreEqual("System.Int32", property.PropertyType.FullName, "The 'EmployeeID' property should be of type int.");
        }

        [Test]
        public void Test_FirstNamePropertyDataType_string()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("FirstName");
            Assert.AreEqual("System.String", property.PropertyType.FullName, "The 'FirstName' property should be of type string.");
        }

        [Test]
        public void Test_LastNamePropertyDataType_string()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("LastName");
            Assert.AreEqual("System.String", property.PropertyType.FullName, "The 'LastName' property should be of type string.");
        }

        [Test]
        public void Test_DepartmentPropertyDataType_string()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("Department");
            Assert.AreEqual("System.String", property.PropertyType.FullName, "The 'Department' property should be of type string.");
        }

        [Test]
        public void Test_SalaryPropertyDataType_decimal()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("Salary");
            Assert.AreEqual("System.Decimal", property.PropertyType.FullName, "The 'Salary' property should be of type decimal.");
        }


        [Test]
        public void Test_EmployeeID_PropertyGetterSetter()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("EmployeeID");
            Assert.IsTrue(property.CanRead, "The 'EmployeeID' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'EmployeeID' property should have a setter.");
        }

        [Test]
        public void Test_FirstName_PropertyGetterSetter()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("FirstName");
            Assert.IsTrue(property.CanRead, "The 'FirstName' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'FirstName' property should have a setter.");
        }

        [Test]
        public void Test_LastName_PropertyGetterSetter()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("LastName");
            Assert.IsTrue(property.CanRead, "The 'LastName' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'LastName' property should have a setter.");
        }

        [Test]
        public void Test_Department_PropertyGetterSetter()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("Department");
            Assert.IsTrue(property.CanRead, "The 'Department' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'Department' property should have a setter.");
        }

        [Test]
        public void Test_Salary_PropertyGetterSetter()
        {
            Type employeeType = assembly.GetType("DisconnectedArchitecture.Models.Employee");
            PropertyInfo property = employeeType.GetProperty("Salary");
            Assert.IsTrue(property.CanRead, "The 'Salary' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'Salary' property should have a setter.");
        }

        [Test]
        public void Test_AddEmployee_ShouldAddEmployee_ToDatabase()
        {
            MethodInfo addEmployeeMethod = programType.GetMethod("AddEmployee", BindingFlags.Public | BindingFlags.Static);
            if (addEmployeeMethod == null)
            {
                Assert.Fail("AddEmployee method not found. Skipping the test.");
            }
            else
            {

                // Arrange
                //InstrumentService instrumentService = new InstrumentService(connectionString);
                Employee testInstrument = new Employee
                {
                    EmployeeID = 999, // Replace with a unique ID that doesn't already exist in the database
                    FirstName = "John",
                    LastName = "Doe",
                    Department = "IT",
                    Salary = 50000
                };
                lastInsertedEmployeeId = testInstrument.EmployeeID;
                // Act
                //Program.AddEmployee(testInstrument);
                addEmployeeMethod.Invoke(null, new object[] { testInstrument });

                //Employee addedInstrument =  (testInstrument.InstrumentId);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Employees where EmployeeID = @InstrumentId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@InstrumentId", testInstrument.EmployeeID);
                    int inserted = (int)command.ExecuteScalar();
                    Console.WriteLine(inserted);
                    Assert.AreEqual(999, inserted);
                }

                DeleteEmployee1(testInstrument.EmployeeID);
            }
        }

        [Test]
        public void Test_UpdateSalary_By_EmployeeName_ShouldUpdateEmployeeInDatabase()
        {
            MethodInfo addEmployeeMethod = programType.GetMethod("AddEmployee", BindingFlags.Public | BindingFlags.Static);
            MethodInfo updateEmployeeSalaryMethod = programType.GetMethod("UpdateEmployeeSalary", BindingFlags.Public | BindingFlags.Static);
            
            if (addEmployeeMethod == null || updateEmployeeSalaryMethod == null)
            {
                Assert.Fail("AddEmployee or UpdateEmployeeSalary method not found.");
            }
            else
            {
                // Arrange
                Employee testEmployee = new Employee
                {
                    EmployeeID = 9993, // Replace with a unique ID that doesn't already exist in the database
                    FirstName = "John",
                    LastName = "Doe",
                    Department = "IT",
                    Salary = 50000
                };
                lastInsertedEmployeeId = testEmployee.EmployeeID;

                // Add the employee to the database initially
                addEmployeeMethod.Invoke(null, new object[] { testEmployee });

                // Modify the testEmployee with updated values
                testEmployee.Salary = 60000;

                // Act
                updateEmployeeSalaryMethod.Invoke(null, new object[] { testEmployee.FirstName, testEmployee.LastName, testEmployee.Salary });

                // Assert
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Salary FROM Employees WHERE FirstName = @FirstName AND LastName = @LastName";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", testEmployee.FirstName);
                    command.Parameters.AddWithValue("@LastName", testEmployee.LastName);
                    decimal updatedSalary = (decimal)command.ExecuteScalar();

                    Assert.AreEqual(testEmployee.Salary, updatedSalary, "Employee salary should be updated in the database.");
                }

                DeleteEmployee(testEmployee.EmployeeID);
            }
        }


        [Test]
        public void Test_DeleteProduct_ShouldDeleteProductFromDatabase()
        {

            MethodInfo addProductMethod = programType.GetMethod("AddProduct", BindingFlags.Public | BindingFlags.Static);
            MethodInfo deleteProductMethod1 = programType.GetMethod("DeleteProduct", BindingFlags.Public | BindingFlags.Static);
            if (addProductMethod == null && deleteProductMethod1 == null)
            {
                Assert.Fail("AddProduct & UpdateProductStock methods not found.");
            }
            else
            {
                // Arrange
                //InstrumentService instrumentService = new InstrumentService(connectionString);
                Product testInstrument = new Product
                {
                    ProductID = 9993, // Replace with a unique ID that doesn't already exist in the database
                    ProductName = "Test Product",
                    Price = 1500,
                    StockQuantity = 50
                };
                lastInsertedProductId = testInstrument.ProductID;

                // Add the instrument to the database initially
                //Program.AddProduct(testInstrument);
                addProductMethod.Invoke(null, new object[] { testInstrument });


                // Act
                //Program.DeleteProduct(testInstrument.ProductID);
                deleteProductMethod1.Invoke(null,new object[] { testInstrument.ProductID });
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Products where ProductID = @InstrumentId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@InstrumentId", testInstrument.ProductID);
                    if (command.ExecuteScalar() == null)
                    {
                        Assert.Pass();
                    }
                    else { Assert.Fail(); }
                }
            }
        }

        [Test]
        public void TestAddProductMethod()
        {
            AssertMethodExists("AddProduct");
        }

        [Test]
        public void TestListProductsMethod()
        {
            AssertMethodExists("ListProducts");
        }

        [Test]
        public void TestUpdateProductStockMethod()
        {
            AssertMethodExists("UpdateProductStock");
        }

        [Test]
        public void TestDeleteProductMethod()
        {
            AssertMethodExists("DeleteProduct");
        }

        private void AssertMethodExists(string methodName)
        {
            MethodInfo method = programType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Assert.NotNull(method, $"Method '{methodName}' not found.");
        }


        private void DeleteProduct1(int productId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.ChangeDatabase(databaseName);

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = $"DELETE FROM {tableName} WHERE ProductID = @ID";
                    command.Parameters.AddWithValue("@ID", productId);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                // Handle any exception if necessary
            }
        }




    }



}
