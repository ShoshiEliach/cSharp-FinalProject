using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BL
{
    public class EmployeesBL
    {
        DBConnection conn = new DBConnection();
        List<Employee> employees;
        List<string> roleInCompany = new List<string>();
        public IEnumerable<Employee> desineEmployee()
        {
            employees = conn.GetAllEmployees();
            return employees;
        }

        public IEnumerable<string> RoleInCompany()
        {
            employees = conn.GetAllEmployees();

            foreach (Employee emp in employees)
            {
                //if(!roleInCompany.Contains(emp.RoleInCompany)||roleInCompany==null)
                roleInCompany.Add(emp.RoleInCompany);
            }
            return roleInCompany;
        }

        public IEnumerable<Employee> FilterRoleInCompany(string role)
        {
            employees = conn.GetAllEmployees();
            List<Employee> filterEmployee = new List<Employee>();
            foreach (Employee emp in employees)
            {
                if (emp.RoleInCompany.Contains(role))
                    filterEmployee.Add(emp);
            }
            return filterEmployee;
        }


        public  bool addEmployee(Employee emp)
        {
            employees = conn.GetAllEmployees();
            foreach (Employee empCheck in employees)
            {
                if (empCheck.Id == emp.Id)
                    return false;
            }
            conn.addNewEmployee(emp);
            return true;
        }
      
    }

}
