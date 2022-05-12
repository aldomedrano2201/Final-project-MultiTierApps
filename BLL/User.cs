using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.DAL;

namespace Final_Project.BLL
{
    public class User
    {
        private int userId;
        private string password;
        private int employeeId;

        public int UserId { get => userId; set => userId = value; }
        public string Password { get => password; set => password = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }

        public void SaveUser(User user)
        {
            UserDB.SaveRecord(user);
        }

        public void UpdateUser(string user, string password)
        {
            UserDB.UpdateRecord(user,password);
        }

        public void DeleteUser(int id)
        {
            UserDB.DeleteRecord(id);
        }

        public User SearchUser(int userValue)
        {
            return UserDB.SearchRecord(userValue);
        }

        public bool IsDuplicateUserEmployeeId(int userId)
        {
            return UserDB.IsDuplicated(userId);
        }

        public List<string> CheckUser(string userName, string password, int flag)
        {
            return UserDB.SearchUserRecord(userName,password,flag);
        }

        public bool UpdateUserOK(string userName, string password, int flag)
        {
            List<string> userEmp = UserDB.SearchUserRecord(userName, password, flag);
            if (userEmp != null)
            {
                UserDB.UpdateRecord(userEmp[0],password);
            }
            else
            {
                return false;
            }
            return true;
        }

        public List<User> GetAllUsers()
        {
            return UserDB.GetAllRecords();
        }

    }
}
