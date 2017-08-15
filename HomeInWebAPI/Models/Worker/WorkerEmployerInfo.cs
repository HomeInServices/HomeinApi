using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models.Worker
{
    public class WorkerEmployerInformation
    {
        public string facebookid { get; set; }
        public string phone { get; set; }
        public int email { get; set; }
        public string name { get; set; }
        public string availability { get; set; }
    }
}