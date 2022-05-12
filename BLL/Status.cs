using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final_Project.DAL;

namespace Final_Project.BLL
{
    public class Status
    {
        private int statusId;
        private string state;

        public int StatusId { get => statusId; set => statusId = value; }
        public string State { get => state; set => state = value; }

        public List<Status> GetAllStatus()
        {
            return StatusDB.GetAllRecords();
        }

        public Status SearchStatus(int staValue)
        {
            return StatusDB.SearchRecord(staValue);
        }

    }
}
