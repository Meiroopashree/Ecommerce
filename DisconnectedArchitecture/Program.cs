using System;
using System.Data;
using System.Data.SqlClient;
using DisconnectedArchitecture.Models;

namespace DisconnectedArchitecture
{
    public static class ConnectionStringProvider
    {
        // Replace with your actual connection string
        public static string ConnectionString { get; } = "User ID=sa;password=examlyMssql@123; server=localhost;Database=appdb;trusted_connection=false;Persist Security Info=False;Encrypt=False";
    }

    public class Program
    {
        // Replace with your actual connection string
        static string connectionString = ConnectionStringProvider.ConnectionString;

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("\nMenu:");
                    Console.WriteLine("1. Add Customer");
                    Console.WriteLine("2. List Customers");
                    Console.WriteLine("3. Update Customer Email");
                    Console.WriteLine("4. Delete Customer");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter your choice (1-5): ");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Customer newCustomer = new Customer();
                            Console.WriteLine("Enter the details for the new customer:");
                            Console.Write("CustomerID: ");
                            newCustomer.CustomerID = int.Parse(Console.ReadLine());
                            Console.Write("FirstName: ");
                            newCustomer.FirstName = Console.ReadLine();
                            Console.Write("LastName: ");
                            newCustomer.LastName = Console.ReadLine();
                            Console.Write("Email: ");
                            newCustomer.Email = Console.ReadLine();
                            Console.Write("PhoneNumber: ");
                            newCustomer.PhoneNumber = Console.ReadLine();
                            AddCustomer(newCustomer);
                            break;

                        case 2:
                            Console.WriteLine("List of Customers:");
                            ListCustomers();
                            break;

                        case 3:
                            Console.Write("Enter the CustomerID of the customer to update email: ");
                            int customerIDToUpdate = int.Parse(Console.ReadLine());
                            Console.Write("Enter the new email: ");
                            string newEmail = Console.ReadLine();
                            UpdateCustomerEmail(customerIDToUpdate, newEmail);
                            break;

                        case 4:
                            Console.Write("Enter the CustomerID of the customer to delete: ");
                            int customerIDToDelete = int.Parse(Console.ReadLine());
                            DeleteCustomer(customerIDToDelete);
                            break;

                        case 5:
                            Console.WriteLine("Exiting the application...");
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a valid option.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static DataSet GetCustomersDataSet()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Customers";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Customers");
                return dataSet;
            }
        }

        private static void UpdateDatabase(DataSet dataSet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Customers";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                connection.Open();

                adapter.Update(dataSet, "Customers");
            }
        }

        // Change your method names (xyz) to appropriate name
        public static void AddCustomer(Customer customer)
        {
            // Write your code to add new customer
            DataSet dataSet = GetCustomersDataSet();

            DataRow newRow = dataSet.Tables["Customers"].NewRow();
            newRow["CustomerID"] = customer.CustomerID;
            newRow["FirstName"] = customer.FirstName;
            newRow["LastName"] = customer.LastName;
            newRow["Email"] = customer.Email;
            newRow["PhoneNumber"] = customer.PhoneNumber;

            dataSet.Tables["Customers"].Rows.Add(newRow);

            UpdateDatabase(dataSet);

            Console.WriteLine("Customer added successfully.");
        }

        public static void ListCustomers()
        {
            // Write your code to list customers
            DataSet dataSet = GetCustomersDataSet();

            foreach (DataRow row in dataSet.Tables["Customers"].Rows)
            {
                Console.WriteLine($"CustomerID: {row["CustomerID"]}, Name: {row["FirstName"]} {row["LastName"]}, " +
                                  $"Email: {row["Email"]}, PhoneNumber: {row["PhoneNumber"]}");
            }
        }

        public static void UpdateCustomerEmail(int customerID, string newEmail)
        {
            // Write your code here to update the email of a customer
            DataSet dataSet = GetCustomersDataSet();

            DataRow[] rows = dataSet.Tables["Customers"].Select($"CustomerID = {customerID}");
            if (rows.Length == 1)
            {
                rows[0]["Email"] = newEmail;
                UpdateDatabase(dataSet);
                Console.WriteLine("Customer email updated successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found or multiple customers with the same ID found.");
            }
        }

        public static void DeleteCustomer(int customerID)
        {
            // Write your code to delete the customer using CustomerID
            DataSet dataSet = GetCustomersDataSet();

            DataRow[] rows = dataSet.Tables["Customers"].Select($"CustomerID = {customerID}");
            if (rows.Length == 1)
            {
                rows[0].Delete();
                UpdateDatabase(dataSet);
                Console.WriteLine("Customer deleted successfully.");
            }
            else
            {
                Console.WriteLine("Customer not found or multiple customers with the same ID found.");
            }
        }
    }
}
