﻿using System;
using System.Data.SqlClient;

namespace dotnetapp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Connection string for EmployeeDB
            string employeeConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=EmployeeDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

            // Connection string for DepartmentDB
            string departmentConnectionString = "User ID=sa;password=examlyMssql@123; server=localhost;Database=DepartmentDB;trusted_connection=false;Persist Security Info=False;Encrypt=False;";

            try
            {
                // Create Employee table
                CreateEmployeeTable(employeeConnectionString);
                Console.WriteLine("Employee table created successfully.");

                // Insert values into Employee table
                InsertEmployee(employeeConnectionString, 1, "John Doe", "john@example.com", "1234567890", "HR");
                InsertEmployee(employeeConnectionString, 2, "Jane Smith", "jane@example.com", "9876543210", "Finance");
                InsertEmployee(employeeConnectionString, 3, "David Brown", "david@example.com", "5555555555", "IT");

               

                // Create Department table
                CreateDepartmentTable(departmentConnectionString);
                Console.WriteLine("Department table created successfully.");

                // Insert values into Department table
                InsertDepartment(departmentConnectionString, 1, "HR", "New York", 10);
                InsertDepartment(departmentConnectionString, 2, "Finance", "Los Angeles", 8);
                InsertDepartment(departmentConnectionString, 3, "IT", "Chicago", 15);

       

                // Delete an Employee
                int employeeIdToDelete = 1; // Specify the employee ID to delete
                DeleteEmployee(employeeConnectionString, employeeIdToDelete);

                // Delete a Department
                int departmentIdToDelete = 1; // Specify the department ID to delete
                DeleteDepartment(departmentConnectionString, departmentIdToDelete);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void CreateEmployeeTable(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = "CREATE TABLE Employee (EmpId INT PRIMARY KEY, EmpName VARCHAR(50), Email VARCHAR(50), PhoneNumber VARCHAR(20), Department VARCHAR(50))";

                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public static void CreateDepartmentTable(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = "CREATE TABLE Department (DeptId INT PRIMARY KEY, DeptName VARCHAR(50), Location VARCHAR(50), EmployeeCount INT)";

                SqlCommand command = new SqlCommand(createTableQuery, connection);
                command.ExecuteNonQuery();
            }
        }

        public static void InsertEmployee(string connectionString, int empId, string empName, string email, string phoneNumber, string department)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = $"INSERT INTO Employee (EmpId, EmpName, Email, PhoneNumber, Department) VALUES ({empId}, '{empName}', '{email}', '{phoneNumber}', '{department}')";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Inserted {rowsAffected} employee(s).");
            }
        }

        public static void InsertDepartment(string connectionString, int deptId, string deptName, string location, int employeeCount)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = $"INSERT INTO Department (DeptId, DeptName, Location, EmployeeCount) VALUES ({deptId}, '{deptName}', '{location}', {employeeCount})";

                SqlCommand command = new SqlCommand(insertQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Inserted {rowsAffected} department(s).");
            }
        }
        public static void DeleteEmployee(string connectionString, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = $"DELETE FROM Employee WHERE EmpId = {employeeId}";

                SqlCommand command = new SqlCommand(deleteQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Deleted {rowsAffected} employee(s).");
            }
        }

        public static void DeleteDepartment(string connectionString, int departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = $"DELETE FROM Department WHERE DeptId = {departmentId}";

                SqlCommand command = new SqlCommand(deleteQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Deleted {rowsAffected} department(s).");
            }
        }
        public static bool RecordExists(string connectionString, string tableName, int id)
        {
            bool exists = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE EmpId = @Id"; // Use 'EmpId' instead of 'Id'
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                int count = (int)command.ExecuteScalar();
                exists = count > 0;
            }
            return exists;
        }
    }
}
