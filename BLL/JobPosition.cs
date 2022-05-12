using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.DAL;

namespace Final_Project.BLL
{
    public class JobPosition
    {
        private int jobId;
        private string jobTitle;

        public int JobId { get => jobId; set => jobId = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }

        public List<JobPosition> GetAllJobPositions()
        {
            return JobPositionDB.GetAllRecords();
        }

        public JobPosition SearchJobPosition(int jobValue)
        {
            return JobPositionDB.SearchRecord(jobValue);
        }

    }
}
