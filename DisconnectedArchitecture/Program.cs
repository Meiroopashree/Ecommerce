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
                    Console.WriteLine("1. Add Employee");
                    Console.WriteLine("2. List Employees");
                    Console.WriteLine("3. Update Employee Salary");
                    Console.WriteLine("4. Delete Employee");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter your choice (1-5): ");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Employee newEmployee = new Employee();
                            Console.WriteLine("Enter the details for the new employee:");
                            Console.Write("EmployeeID: ");
                            newEmployee.EmployeeID = int.Parse(Console.ReadLine());
                            Console.Write("FirstName: ");
                            newEmployee.FirstName = Console.ReadLine();
                            Console.Write("LastName: ");
                            newEmployee.LastName = Console.ReadLine();
                            Console.Write("Department: ");
                            newEmployee.Department = Console.ReadLine();
                            Console.Write("Salary: ");
                            newEmployee.Salary = decimal.Parse(Console.ReadLine());
                            AddEmployee(newEmployee);
                            break;

                        case 2:
                            Console.WriteLine("List of Employees:");
                            ListEmployees();
                            break;

                        case 3:
                            Console.Write("Enter the EmployeeID of the employee to update salary: ");
                            int employeeIDToUpdate = int.Parse(Console.ReadLine());
                            Console.Write("Enter the new salary: ");
                            decimal newSalary = decimal.Parse(Console.ReadLine());
                            UpdateEmployeeSalary(employeeIDToUpdate, newSalary);
                            break;

                        case 4:
                            Console.Write("Enter the EmployeeID of the employee to delete: ");
                            int employeeIDToDelete = int.Parse(Console.ReadLine());
                            DeleteEmployee(employeeIDToDelete);
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

        private static DataSet GetEmployeesDataSet()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Employees");
                return dataSet;
            }
        }

        private static void UpdateDatabase(DataSet dataSet)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                connection.Open();

                adapter.Update(dataSet, "Employees");
            }
        }

        public static void AddEmployee(Employee employee)
        {
            DataSet dataSet = GetEmployeesDataSet();

            DataRow newRow = dataSet.Tables["Employees"].NewRow();
            newRow["EmployeeID"] = employee.EmployeeID;
            newRow["FirstName"] = employee.FirstName;
            newRow["LastName"] = employee.LastName;
            newRow["Department"] = employee.Department;
            newRow["Salary"] = employee.Salary;

            dataSet.Tables["Employees"].Rows.Add(newRow);

            UpdateDatabase(dataSet);

            Console.WriteLine("Employee added successfully.");
        }

        public static void ListEmployees()
        {
            DataSet dataSet = GetEmployeesDataSet();

            foreach (DataRow row in dataSet.Tables["Employees"].Rows)
            {
                Console.WriteLine($"EmployeeID: {row["EmployeeID"]}, Name: {row["FirstName"]} {row["LastName"]}, " +
                                  $"Department: {row["Department"]}, Salary: {row["Salary"]}");
            }
        }

        public static void UpdateEmployeeSalary(int employeeID, decimal newSalary)
        {
            DataSet dataSet = GetEmployeesDataSet();

            DataRow[] rows = dataSet.Tables["Employees"].Select($"EmployeeID = {employeeID}");
            if (rows.Length == 1)
            {
                rows[0]["Salary"] = newSalary;
                UpdateDatabase(dataSet);
                Console.WriteLine("Employee salary updated successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found or multiple employees with the same ID found.");
            }
        }

        public static void DeleteEmployee(int employeeID)
        {
            DataSet dataSet = GetEmployeesDataSet();

            DataRow[] rows = dataSet.Tables["Employees"].Select($"EmployeeID = {employeeID}");
            if (rows.Length == 1)
            {
                rows[0].Delete();
                UpdateDatabase(dataSet);
                Console.WriteLine("Employee deleted successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found or multiple employees with the same ID found.");
            }
        }
    }
}
