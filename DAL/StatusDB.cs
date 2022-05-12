using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.BLL;
using System.Data.SqlClient;

namespace Final_Project.DAL
{
    public static class StatusDB
    {
        public static List<Status> GetAllRecords()
        {
            SqlConnection conn = new SqlConnection();
            List<Status> listS = new List<Status>();
            Status sta;
            try
            {
                conn = UtilityDB.ConnectDB();






                SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM StatuS", conn);
                SqlDataReader reader = cmdSelectAll.ExecuteReader();
                while (reader.Read())
                {
                    sta = new Status();
                    sta.StatusId = Convert.ToInt32(reader["StatusId"]);
                    sta.State = reader["State"].ToString();
                    listS.Add(sta);
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

            return listS;
        }

        public static Status SearchRecord(int value)
        {
            Status sta = new Status();
            SqlConnection conn = UtilityDB.ConnectDB();
            //your code here
            try
            {
                SqlCommand cmdSearchById = new SqlCommand();
                cmdSearchById.Connection = conn;


                cmdSearchById.CommandText = "SELECT  * FROM StatuS " +
                "WHERE StatusID = @StatusId";
                cmdSearchById.Parameters.AddWithValue("@StatusId", Convert.ToInt32(value));


                SqlDataReader reader = cmdSearchById.ExecuteReader();
                if (reader.Read())
                {
                    sta.StatusId = Convert.ToInt32(reader["StatusId"]);
                    sta.State = reader["State"].ToString();
                   
                }
                else
                {
                    sta = null;
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


            return sta;
        }
    }
}
