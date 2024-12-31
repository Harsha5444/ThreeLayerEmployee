using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using Employee.Domain;
using System.Data;
using System;

namespace Employee.DAL
{
    public class EmployeeDAL
    {
        public SqlConnection _connection { set; get; }
        private SqlCommand _command;
        public List<EmployeeModel> Employees { get; set; }
        public EmployeeDAL()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeCon"].ConnectionString);
            Employees = new List<EmployeeModel>();
        }
        public List<EmployeeModel> GetEmployees()
        {
            Employees.Clear();
            _command = new SqlCommand("select * from Employee", _connection);
            _connection.Open();
            SqlDataReader dr =  _command.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    EmployeeModel emp = new EmployeeModel()
                    {
                        Eno = Convert.ToInt32(dr["Eno"]),
                        Ename = Convert.ToString(dr["Ename"]),
                        Job = Convert.ToString(dr["Job"]),
                        Salary = Convert.ToDouble(dr["Salary"]),
                        Dname = Convert.ToString(dr["Dname"])
                    };
                    Employees.Add(emp);
                }
                _connection.Close();
                return Employees;
            }
            return new List<EmployeeModel>();
        }
        public string InsertEmployee(EmployeeModel employee)
        {
            string query = $"insert into Employee values({employee.Eno},'{employee.Ename}','{employee.Job}',{employee.Salary},'{employee.Dname}')";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            SqlDataReader dr = _command.ExecuteReader();
            string msg = dr.RecordsAffected+" Record(s) Affected";
            _connection.Close();
            return msg;
        }
        public string UpdateEmployee(int eno)
        {
            Console.WriteLine("Enter Employee Number (Eno): ");
            int enom = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new Employee Name (Ename): ");
            string ename = Console.ReadLine();
            Console.WriteLine("Enter new Job: ");
            string job = Console.ReadLine();
            Console.WriteLine("Enter new Salary: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter new Department Name (Dname): ");
            string dname = Console.ReadLine();
            _command = new SqlCommand("usp_update_employee", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@eno", enom);
            _command.Parameters.AddWithValue("@ename", ename);
            _command.Parameters.AddWithValue("@job", job);
            _command.Parameters.AddWithValue("@salary", salary);
            _command.Parameters.AddWithValue("@dname", dname);
            _connection.Open();
            string result = _command.ExecuteScalar().ToString();
            _connection.Close();
            return result;
        }
        public string DeleteEmployee(EmployeeModel employee)
        {
            _command = new SqlCommand("usp_delete_employee", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@eno", employee.Eno);
            _connection.Open();
            string result = _command.ExecuteScalar().ToString();
            _connection.Close();
            return result;
        }
        public List<EmployeeModel> SortEmployee(string columnName, string order)
        {
            List<EmployeeModel> sortedEmployees = new List<EmployeeModel>();
            string query = $"select * from employee order by {columnName} {order}";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            SqlDataReader dr = _command.ExecuteReader();
            while (dr.Read())
            {
                EmployeeModel emp = new EmployeeModel()
                {
                    Eno = Convert.ToInt32(dr["Eno"]),
                    Ename = Convert.ToString(dr["Ename"]),
                    Job = Convert.ToString(dr["Job"]),
                    Salary = Convert.ToDouble(dr["Salary"]),
                    Dname = Convert.ToString(dr["Dname"])
                };
                sortedEmployees.Add(emp);
            }
            _connection.Close();
            return sortedEmployees;
        }
    }
}
