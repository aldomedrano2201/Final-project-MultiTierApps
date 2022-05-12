using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.DAL;

namespace Final_Project.BLL
{
    public class Employee
    {
        //private class variables
        private int employeeId;
        private string firstName;
        private string lastName;
        private string phoneNumber;
        private string email;
        private int jobId;
        private int statusId;

        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        public int JobId { get => jobId; set => jobId = value; }
        public int StatusId { get => statusId; set => statusId = value; }

        public void SaveEmployee(Employee emp)
        {
            EmployeeDB.SaveRecord(emp);
        }

        public void UpdateEmployee(Employee emp)
        {
            EmployeeDB.UpdateRecord(emp);
        }

        public void DeleteEmployee(int id)
        {
            EmployeeDB.DeleteRecord(id);
        }

        public Employee SearchEmployee(string empValue)
        {
            return EmployeeDB.SearchRecord(empValue);
        }

        public bool IsDuplicateEmpId(string empId)
        {
            return EmployeeDB.IsDuplicated(empId);
        }

        public bool IsDuplicateEmpEmail(string value)
        {
            return EmployeeDB.IsDuplicated(value);
        }


        public List<Employee> GetAllEmployees()
        {
            return EmployeeDB.GetAllRecords();
        }
    }
}
