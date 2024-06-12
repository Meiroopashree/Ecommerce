using NUnit.Framework;
using System;
using System.IO;
using System.Data.SqlClient;
using DisconnectedArchitecture;
using DisconnectedArchitecture.Models;
using System.Reflection;
using DisconnectedArchitecture;
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
        private int lastInsertedCustomerId; // Store the ID of the last inserted Customer

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
            tableName = "Customers";

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            DeleteCustomer1(lastInsertedCustomerId);
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
        public void Test_CustomerClassExists()
        {
            string className = "DisconnectedArchitecture.Models.Customer";
            Type CustomerType = assembly.GetType(className);
            Assert.NotNull(CustomerType, $"The class '{className}' does not exist in the assembly.");
        }

        [Test]
        public void Test_CustomerIDPropertyDataType_Int()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("CustomerID");
            Assert.AreEqual("System.Int32", property.PropertyType.FullName, "The 'CustomerID' property should be of type int.");
        }

        [Test]
        public void Test_FirstNamePropertyDataType_string()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("FirstName");
            Assert.AreEqual("System.String", property.PropertyType.FullName, "The 'FirstName' property should be of type string.");
        }

        [Test]
        public void Test_LastNamePropertyDataType_string()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("LastName");
            Assert.AreEqual("System.String", property.PropertyType.FullName, "The 'LastName' property should be of type string.");
        }

        [Test]
        public void Test_EmailPropertyDataType_string()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("Email");
            Assert.AreEqual("System.String", property.PropertyType.FullName, "The 'Email' property should be of type string.");
        }

        [Test]
        public void Test_PhoneNumberPropertyDataType_string()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("PhoneNumber");
            Assert.AreEqual("System.String", property.PropertyType.FullName, "The 'PhoneNumber' property should be of type string.");
        }


        [Test]
        public void Test_CustomerID_PropertyGetterSetter()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("CustomerID");
            Assert.IsTrue(property.CanRead, "The 'CustomerID' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'CustomerID' property should have a setter.");
        }

        [Test]
        public void Test_FirstName_PropertyGetterSetter()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("FirstName");
            Assert.IsTrue(property.CanRead, "The 'FirstName' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'FirstName' property should have a setter.");
        }

        [Test]
        public void Test_LastName_PropertyGetterSetter()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("LastName");
            Assert.IsTrue(property.CanRead, "The 'LastName' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'LastName' property should have a setter.");
        }

        [Test]
        public void Test_Email_PropertyGetterSetter()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("Email");
            Assert.IsTrue(property.CanRead, "The 'Email' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'Email' property should have a setter.");
        }

        [Test]
        public void Test_PhoneNumber_PropertyGetterSetter()
        {
            Type customerType = assembly.GetType("DisconnectedArchitecture.Models.Customer");
            PropertyInfo property = customerType.GetProperty("PhoneNumber");
            Assert.IsTrue(property.CanRead, "The 'PhoneNumber' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'PhoneNumber' property should have a setter.");
        }


        [Test]
        public void Test_AddCustomer_ShouldAddCustomer_ToDatabase()
        {
            MethodInfo addCustomerMethod = programType.GetMethod("AddCustomer", BindingFlags.Public | BindingFlags.Static);
            if (addCustomerMethod == null)
            {
                Assert.Fail("AddCustomer method not found. Skipping the test.");
            }
            else
            {

                // Arrange
                //InstrumentService instrumentService = new InstrumentService(connectionString);
                Customer testInstrument = new Customer
                {
                    CustomerID = 999, // Replace with a unique ID that doesn't already exist in the database
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "9876543210"
                };
                lastInsertedCustomerId = testInstrument.CustomerID;
                // Act
                //Program.AddCustomer(testInstrument);
                addCustomerMethod.Invoke(null, new object[] { testInstrument });

                //Customer addedInstrument =  (testInstrument.InstrumentId);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Customers where CustomerID = @InstrumentId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@InstrumentId", testInstrument.CustomerID);
                    int inserted = (int)command.ExecuteScalar();
                    Console.WriteLine(inserted);
                    Assert.AreEqual(999, inserted);
                }

                DeleteCustomer1(testInstrument.CustomerID);
            }
        }

        [Test]
        public void Test_UpdateEmail_By_CustomerID_ShouldUpdateCustomerInDatabase()
        {
            MethodInfo addCustomerMethod = programType.GetMethod("AddCustomer", BindingFlags.Public | BindingFlags.Static);
            MethodInfo updateCustomerMethod1 = programType.GetMethod("UpdateCustomerEmail", BindingFlags.Public | BindingFlags.Static);
            if (addCustomerMethod == null || updateCustomerMethod1 == null)
            {
                Assert.Fail("AddCustomer or UpdateCustomerEmail methods not found.");
            }
            else
            {
                // Arrange
                Customer testCustomer = new Customer
                {
                    CustomerID = 999, // Replace with a unique ID that doesn't already exist in the database
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "9876543210"
                };
                lastInsertedCustomerId = testCustomer.CustomerID;

                // Add the Customer to the database initially
                addCustomerMethod.Invoke(null, new object[] { testCustomer });

                // Modify the testCustomer with updated values
                string newEmail = "new.email@example.com";
                
                // Act
                updateCustomerMethod1.Invoke(null, new object[] { testCustomer.CustomerID, newEmail });

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Email FROM Customers WHERE CustomerID = @CustomerID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", testCustomer.CustomerID);
                    string updatedEmail = (string)command.ExecuteScalar();

                    // Assert
                    Assert.AreEqual(newEmail, updatedEmail, "Customer Email should be updated in the database.");
                }

                DeleteCustomer1(testCustomer.CustomerID);
            }
        }




        [Test]
        public void Test_DeleteCustomer_ShouldDeleteCustomerFromDatabase()
        {

            MethodInfo addCustomerMethod = programType.GetMethod("AddCustomer", BindingFlags.Public | BindingFlags.Static);
            MethodInfo deleteCustomerMethod1 = programType.GetMethod("DeleteCustomer", BindingFlags.Public | BindingFlags.Static);
            if (addCustomerMethod == null && deleteCustomerMethod1 == null)
            {
                Assert.Fail("AddCustomer & UpdateCustomerEmail methods not found.");
            }
            else
            {
                // Arrange
                //InstrumentService instrumentService = new InstrumentService(connectionString);
                Customer testInstrument = new Customer
                {
                    CustomerID = 999, // Replace with a unique ID that doesn't already exist in the database
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "9876543210"
                };
                lastInsertedCustomerId = testInstrument.CustomerID;

                // Add the instrument to the database initially
                //Program.AddCustomer(testInstrument);
                addCustomerMethod.Invoke(null, new object[] { testInstrument });


                // Act
                //Program.DeleteCustomer(testInstrument.CustomerID);
                deleteCustomerMethod1.Invoke(null,new object[] { testInstrument.CustomerID });
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Customers where CustomerID = @InstrumentId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@InstrumentId", testInstrument.CustomerID);
                    if (command.ExecuteScalar() == null)
                    {
                        Assert.Pass();
                    }
                    else { Assert.Fail(); }
                }
            }
        }

        [Test]
        public void TestAddCustomerMethod()
        {
            AssertMethodExists("AddCustomer");
        }

        [Test]
        public void TestListCustomersMethod()
        {
            AssertMethodExists("ListCustomers");
        }

        [Test]
        public void TestUpdateCustomerEmailMethod()
        {
            AssertMethodExists("UpdateCustomerEmail");
        }

        [Test]
        public void TestDeleteCustomerMethod()
        {
            AssertMethodExists("DeleteCustomer");
        }

        private void AssertMethodExists(string methodName)
        {
            MethodInfo method = programType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
            Assert.NotNull(method, $"Method '{methodName}' not found.");
        }


        private void DeleteCustomer1(int CustomerId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.ChangeDatabase(databaseName);

                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = $"DELETE FROM {tableName} WHERE CustomerID = @ID";
                    command.Parameters.AddWithValue("@ID", CustomerId);

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
