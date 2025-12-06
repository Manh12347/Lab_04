using MIS_Lab4.Models;
using System;
using System.Collections.Generic;

namespace MIS_Lab4.Services
{
    public class EmployeeService
    {
        private Dictionary<string, Employee> employees;

        public EmployeeService()
        {
            employees = new Dictionary<string, Employee>();
        }

        public bool AddEmployee(Employee employee)
        {
            if (employees.ContainsKey(employee.Id))
            {
                return false; // Trùng ID
            }

            employees[employee.Id] = employee;
            return true;
        }

        public bool DeleteEmployee(string id)
        {
            if (!employees.ContainsKey(id))
            {
                return false; // Không tồn tại
            }

            employees.Remove(id);
            return true;
        }

        public bool UpdateEmployee(Employee employee)
        {
            if (!employees.ContainsKey(employee.Id))
            {
                return false; // Không tồn tại
            }

            employees[employee.Id] = employee;
            return true;
        }

        public Employee GetEmployeeById(string id)
        {
            if (employees.ContainsKey(id))
            {
                return employees[id];
            }

            return null;
        }

        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>(employees.Values);
        }
    }
}
