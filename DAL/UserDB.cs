using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.BLL;
using System.Data.SqlClient;

namespace Final_Project.DAL
{
    public static class UserDB
    {

        public static void SaveRecord(User user)
        {
            //using store procedure

            //connect DB
            SqlConnection conn = UtilityDB.ConnectDB();
            //create and customize an object of type sqlCommand



            try
            {
                SqlCommand cmdInsert = new SqlCommand("SP_SaveUser", conn);
                cmdInsert.Connection = conn;
                cmdInsert.Parameters.AddWithValue("@EmployeeId", user.EmployeeId);
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.ExecuteNonQuery();


            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //close the DB
                conn.Close();
                conn.Dispose();
            }






        }

        public static void UpdateRecord(string empId, string password)
        {
            //using store procedure

            //connect DB
            SqlConnection conn = UtilityDB.ConnectDB();
            //create and customize an object of type sqlCommand



            try
            {
                SqlCommand cmdInsert = new SqlCommand("SP_UpdateUser", conn);
                cmdInsert.Connection = conn;
                cmdInsert.Parameters.AddWithValue("@EmployeeId", Convert.ToInt32( empId));
                cmdInsert.Parameters.AddWithValue("@Password", password);
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.ExecuteNonQuery();


            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //close the DB
                conn.Close();
                conn.Dispose();
            }



        }

        public static void DeleteRecord(int id)
        {
            //using store procedure

            //connect DB
            SqlConnection conn = UtilityDB.ConnectDB();
            //create and customize an object of type sqlCommand



            try
            {
                SqlCommand cmdInsert = new SqlCommand("SP_DeleteUser", conn);
                cmdInsert.Connection = conn;
                cmdInsert.Parameters.AddWithValue("@UserId", id);
                cmdInsert.CommandType = System.Data.CommandType.StoredProcedure;
                cmdInsert.ExecuteNonQuery();


            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //close the DB
                conn.Close();
                conn.Dispose();
            }






        }

        public static User SearchUserRecord(string value)
        {
            User user = new User();
            SqlConnection conn = UtilityDB.ConnectDB();
            //your code here
            try
            {
                SqlCommand cmdSearchById = new SqlCommand();
                cmdSearchById.Connection = conn;

                 cmdSearchById.CommandText = "SELECT  * FROM USERSACCOUNT" +
                    "WHERE UserID = @UserId";
                    cmdSearchById.Parameters.AddWithValue("@UserId", Convert.ToInt32(value));
               

                SqlDataReader reader = cmdSearchById.ExecuteReader();
                if (reader.Read())
                {
                   
                    user.Password = reader["Password"].ToString();
                    user.UserId = Convert.ToInt32(reader["UserId"]);

                }
                else
                {
                    user = null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //close the DB
                conn.Close();
                conn.Dispose();
            }


            return user;
        }

        public static List<string> SearchUserRecord(string userName, string password, int flag)
        {
            List<string> empUser = new List<string>();
            SqlConnection conn = UtilityDB.ConnectDB();
            //your code here
            try
            {
                SqlCommand cmdSearchById = new SqlCommand();
                cmdSearchById.Connection = conn;

                cmdSearchById.CommandText = "SELECT * FROM EMPLOYEES " +
                   "WHERE Email = @Email";
                cmdSearchById.Parameters.AddWithValue("@Email", userName.ToLower());


                SqlDataReader reader = cmdSearchById.ExecuteReader();
                if (reader.Read())
                {
                    empUser.Add(reader["EmployeeId"].ToString());
                    empUser.Add(reader["JobId"].ToString());
                    empUser.Add(reader["StatusId"].ToString());
                    conn.Close();
                    conn.Dispose();
                    conn = UtilityDB.ConnectDB();


                    cmdSearchById.Connection = conn;
                    cmdSearchById.CommandText = "SELECT * FROM USERSACCOUNT " +
                        "WHERE EmployeeId = @EmployeeId and Password = @Password";
                    cmdSearchById.Parameters.AddWithValue("@EmployeeId", Convert.ToInt32(empUser[0]));
                    cmdSearchById.Parameters.AddWithValue("@Password", password);
                    SqlDataReader userReader = cmdSearchById.ExecuteReader();
                    if (userReader.Read())
                    {
                        if (userReader["Password"].ToString() == "")
                        {
                            return empUser;
                        }
                        else if (userReader["Password"].ToString() == password)
                        {
                            return empUser;
                        }
                        
                        else
                        {
                            return empUser = null;
                        }
                        
                    }
                    else if (flag == 1)
                    {
                        return empUser;
                    }
                    else
                    {
                        return empUser = null;
                    }
                   

                }
                else
                {
                    empUser = null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //close the DB
                conn.Close();
                conn.Dispose();
            }


            return empUser;
        }

       
        public static bool IsDuplicated(int id)
        {
            if (UserDB.SearchRecord(id) != null)
                return true;

            return false;
        }

        public static User SearchRecord(int input)
        {

            User user = new User();
            SqlConnection conn = UtilityDB.ConnectDB();
            //your code here
            try
            {
                SqlCommand cmdSearchById = new SqlCommand();
                cmdSearchById.Connection = conn;


                cmdSearchById.CommandText = "SELECT  * FROM USERSACCOUNT " +
                "WHERE EmployeeID = @EmployeeId";
                cmdSearchById.Parameters.AddWithValue("@EmployeeId", Convert.ToInt32(input));


                SqlDataReader reader = cmdSearchById.ExecuteReader();
                if (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    
                }
                else
                {
                    user = null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                //close the DB
                conn.Close();
                conn.Dispose();
            }


            return user;
        }

        public static List<User> GetAllRecords()
        {
            SqlConnection conn = new SqlConnection();
            List<User> listE = new List<User>();
            User user;
            try
            {
                conn = UtilityDB.ConnectDB();






                SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM USERSACCOUNT", conn);
                SqlDataReader reader = cmdSelectAll.ExecuteReader();
                while (reader.Read())
                {
                    user = new User();
                    user.UserId = Convert.ToInt32(reader["UserId"]);
                    user.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    listE.Add(user);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            return listE;
        }

    }
}
