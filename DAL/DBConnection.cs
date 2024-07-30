using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DBConnection
    {
        InterviewsManagerContext imContext=new InterviewsManagerContext();
        public List<Employee> GetAllEmployees()
        {
            return imContext.Employees.ToList();
        } 
        public List<Candidate> GetAllCandidates()
        {
            return imContext.Candidates.ToList();
        } 
        public List<Interview> GetAllInterviews()
        {
            return imContext.Interviews.ToList();
        }

        public void addNewEmployee(Employee employee)
        {
            List<Employee> newlistEmployee = imContext.Employees.ToList();
            newlistEmployee.Add(employee);
            DbSet<Employee> newEmployeeSet = imContext.Set<Employee>();
            newEmployeeSet.AddRange(newlistEmployee);
            imContext.SaveChanges();
        }
    }
}
