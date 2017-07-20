using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models.Worker
{
    public class WorkerSchedule
    {
        WorkerSchedule()
        {
            startdate = new DateTime();
            enddate = new DateTime();
            description = "";
        }
        public System.DateTime startdate { get; set; }
        public System.DateTime enddate { get; set; }
        public string description { get; set; }
        public int worker_id { get; set; }
        public int user_id { get; set; }
    }
}