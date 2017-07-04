using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models
{

    public class TransactionModel
    {
        public string _id { get; set; }
        public string paymentType { get; set; }
        public string user_id { get; set; }
        public string worker_id { get; set; }
        public string amount { get; set; }
        public Skillsused[] skillsUsed { get; set; }
    }

    public class Skillsused
    {
        public string spotclean { get; set; }
    }

}