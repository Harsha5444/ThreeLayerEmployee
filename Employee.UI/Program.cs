using Employee.BAL;
using Employee.Domain;
using System;
using System.Collections.Generic;

namespace Employee.UI
{
    internal class Program
    {
        static EmployeeBAL objBAL;
        static Program()
        {
            objBAL = new EmployeeBAL();
        }
        static EmployeeModel GetInputEmp()
        {
            Console.WriteLine("Enter Employee Details: ");
            return new EmployeeModel()
            {
                Eno = Convert.ToInt32(Console.ReadLine()),
                Ename = Console.ReadLine(),
                Job = Console.ReadLine(),
                Salary = Convert.ToDouble(Console.ReadLine()),
                Dname = Console.ReadLine()
            };
        }
        static string InsertEmployee()
        {
            EmployeeModel emp = GetInputEmp();
            string res = objBAL.InsertEmployee(emp);
            Console.WriteLine(res);
            Console.WriteLine("Updated Table: ");
            List<EmployeeModel> emps = objBAL.GetEmployees();
            Display(emps);
            return res;
        }
        static string UpdateEmployee()
        {
            Console.WriteLine("Enter Employee Eno to Update: ");
            int eno = Convert.ToInt32(Console.ReadLine());
            string res = objBAL.UpdateEmployee(eno);
            Console.WriteLine(res);
            Console.WriteLine("Updated Table: ");
            List<EmployeeModel> emps = objBAL.GetEmployees();
            Display(emps);
            return res;
        }
        static string DeleteEmployee()
        {
            Console.WriteLine("Enter Employee Eno to Delete: ");
            int eno = Convert.ToInt32(Console.ReadLine());
            EmployeeModel emp = new EmployeeModel() { Eno = eno };
            string res = objBAL.DeleteEmployee(emp);
            Console.WriteLine(res);
            Console.WriteLine("Updated Table: ");
            List<EmployeeModel> emps = objBAL.GetEmployees();
            Display(emps);
            return res;
        }
        static void SortEmployee()
        {
            Console.WriteLine("Select column to sort by: ");
            Console.WriteLine("1. Eno\n2. Ename\n3. Job\n4. Salary\n5. Dname");
            int columnChoice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Select sorting order: ");
            Console.WriteLine("1. Ascending\n2. Descending");
            int orderChoice = Convert.ToInt32(Console.ReadLine());
            string columnName = "";
            switch (columnChoice)
            {
                case 1:
                    columnName = "Eno";
                    break;
                case 2:
                    columnName = "Ename";
                    break;
                case 3:
                    columnName = "Job";
                    break;
                case 4:
                    columnName = "Salary";
                    break;
                case 5:
                    columnName = "Dname";
                    break;
                default:
                    columnName = "Eno";
                    break;
            }
            string order = orderChoice == 1 ? "ASC" : "DESC";
            List<EmployeeModel> sortedEmployees = objBAL.SortEmployee(columnName, order);
            Console.WriteLine("Sorted Table: ");
            Display(sortedEmployees);
            Console.WriteLine("Table sorted successfully.");
        }

        static void Display(List<EmployeeModel> employees)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-10} {4,-20}", "Eno", "Ename", "Job", "Salary", "Dname");
            Console.WriteLine(new string('-', 75));
            foreach (EmployeeModel e in employees)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-20} {3,-10} {4,-20}", e.Eno, e.Ename, e.Job, e.Salary, e.Dname);
            }
        }
        static void Main(string[] args)
        {            
            List<EmployeeModel> emps = objBAL.GetEmployees();
            Display(emps);
            Console.WriteLine("\nSelect operation: ");
            Console.WriteLine("1.Insert\n2.Update\n3.Delete\n4.SortTable");
            int dbChoice = Convert.ToInt32(Console.ReadLine());
            switch (dbChoice)
            {
                case 1:
                    InsertEmployee();
                    break;
                case 2:
                    UpdateEmployee();
                    break;
                case 3:
                    DeleteEmployee();
                    break;
                case 4:
                    SortEmployee();
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
            Console.Read();
        }        
    }
}
