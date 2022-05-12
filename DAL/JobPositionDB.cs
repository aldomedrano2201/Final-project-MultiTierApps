using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.BLL;
using System.Data.SqlClient;

namespace Final_Project.DAL
{
    public static class JobPositionDB
    {
        public static List<JobPosition> GetAllRecords()
        {
            SqlConnection conn = new SqlConnection();
            List<JobPosition> listJ = new List<JobPosition>();
            JobPosition job;
            try
            {
                conn = UtilityDB.ConnectDB();






                SqlCommand cmdSelectAll = new SqlCommand("SELECT * FROM JobPositions", conn);
                SqlDataReader reader = cmdSelectAll.ExecuteReader();
                while (reader.Read())
                {
                    job = new JobPosition();
                    job.JobId = Convert.ToInt32(reader["JobId"]);
                    job.JobTitle = reader["jobTitle"].ToString();
                    listJ.Add(job);
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

            return listJ;
        }

        public static JobPosition SearchRecord(int value)
        {
            JobPosition job = new JobPosition();
            SqlConnection conn = UtilityDB.ConnectDB();
            //your code here
            try
            {
                SqlCommand cmdSearchById = new SqlCommand();
                cmdSearchById.Connection = conn;


                cmdSearchById.CommandText = "SELECT  * FROM JobPositions " +
                "WHERE JobID = @JobId";
                cmdSearchById.Parameters.AddWithValue("@JobId", Convert.ToInt32(value));


                SqlDataReader reader = cmdSearchById.ExecuteReader();
                if (reader.Read())
                {
                    job.JobId = Convert.ToInt32(reader["JobId"]);
                    job.JobTitle = reader["JobTitle"].ToString();

                }
                else
                {
                    job = null;
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


            return job;
        }

    }
}
