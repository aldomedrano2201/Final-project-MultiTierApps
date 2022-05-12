using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.BLL;
using System.Data.SqlClient;

namespace Final_Project.DAL
{
    public static class EmployeeDB
    {
       
        public static void SaveRecord(Employee emp)
        {
            //using store procedure

            //connect DB
            SqlConnection conn = UtilityDB.ConnectDB();
            //create and customize an object of type sqlCommand



            try
            {
                SqlCommand cmdInsert = new SqlCommand("SP_SaveEmployee", conn);
                cmdInsert.Connection = conn;
                cmdInsert.Parameters.AddWithValue("@FirstName", emp.FirstName);
                cmdInsert.Parameters.AddWithValue("@LastName", emp.LastName);
                cmdInsert.Parameters.AddWithValue("@JobId", emp.JobId);
                cmdInsert.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                cmdInsert.Parameters.AddWithValue("@Email", emp.Email);
                cmdInsert.Parameters.AddWithValue("@StatusId", emp.StatusId);
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

        public static void UpdateRecord(Employee emp)
        {
            //using store procedure

            //connect DB
            SqlConnection conn = UtilityDB.ConnectDB();
            //create and customize an object of type sqlCommand



            try
            {
                SqlCommand cmdInsert = new SqlCommand("SP_UpdateEmployee", conn);
                cmdInsert.Connection = conn;
                cmdInsert.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmdInsert.Parameters.AddWithValue("@FirstName", emp.FirstName);
                cmdInsert.Parameters.AddWithValue("@LastName", emp.LastName);
                cmdInsert.Parameters.AddWithValue("@JobId", emp.JobId);
                cmdInsert.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                cmdInsert.Parameters.AddWithValue("@Email", emp.Email);
                cmdInsert.Parameters.AddWithValue("@StatusId", emp.StatusId);
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
                SqlCommand cmdInsert = new SqlCommand("SP_DeleteEmployee", conn);
                cmdInsert.Connection = conn;
                cmdInsert.Parameters.AddWithValue("@EmployeeId", id);
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

        public static Employee SearchRecord(string value)
        {
            Employee emp = new Employee();
            SqlConnection conn = UtilityDB.ConnectDB();
            //your code here
            try
            {
                SqlCommand cmdSearchById = new SqlCommand();
                cmdSearchById.Connection = conn;

               if (value.Contains('@'))
                {
                    cmdSearchById.CommandText = "SELECT  * FROM EMPLOYEES " +
                   "WHERE EMAIL = @Email";
                    cmdSearchById.Parameters.AddWithValue("@Email", value);
                }

                else
                {
                    cmdSearchById.CommandText = "SELECT  * FROM EMPLOYEES " +
                  "WHERE EMPLOYEEID = @EMPLOEYEEID";
                    cmdSearchById.Parameters.AddWithValue("@EMPLOEYEEID", Convert.ToInt32(value));
                }

                SqlDataReader reader = cmdSearchById.ExecuteReader();
                if (reader.Read())
                {
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.LastName = reader["LastName"].ToString();
                    emp.PhoneNumber = reader["PhoneNumber"].ToString();
                    emp.Email = reader["Email"].ToString();
                    emp.StatusId = Convert.ToInt32( reader["StatusId"].ToString());
                    emp.JobId = Convert.ToInt32(reader["JobId"].ToString());
                }
                else
                {
                    emp = null;
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


            return emp;
        }

        public static bool IsDuplicated(string value)
        {
            if (EmployeeDB.SearchRecord(value) != null)
                return true;

            return false;
        }

        public static bool IsDuplicated(string id, string email)
        {
            
            if (EmployeeDB.SearchRecord(id) != null)
                return EmployeeDB.SearchRecord(id).Email == email;

            return false;
        }

        public static List<Employee> SearchRecord(string input, int option)
        {
            //Note: option: 1 and 2- search by first name/last name; 2- search by jobtitle
            List<Employee> listE = new List<Employee>();



            return listE;
        }

        public static List<Employee> GetAllRecords()
        {
            SqlConnection conn = new SqlConnection();
            List<Employee> listE = new List<Employee>();
            Employee emp;
            try
            {
                conn = UtilityDB.ConnectDB();






                SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM EMPLOYEES", conn);
                SqlDataReader reader = cmdSelectAll.ExecuteReader();
                while (reader.Read())
                {
                    emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.LastName = reader["LastName"].ToString();
                    emp.PhoneNumber = reader["PhoneNumber"].ToString();
                    emp.Email = reader["Email"].ToString();
                    listE.Add(emp);
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
