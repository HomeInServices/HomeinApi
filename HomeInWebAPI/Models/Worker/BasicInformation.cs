using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models.Worker
{
    public class BasicInformation
    {
        //BasicInformation()
        //{
        //    facebookid = "";
        //    phone = "";
        //    MilesWantToDrive = 0;
        //    street = "";
        //    city = "";
        //    state = "";
        //    country = "";
        //    zipcode = "";
        //}

        public string facebookid { get; set; }
        public string phone { get; set; }
        public int MilesWantToDrive { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string zipcode { get; set; }
    }
}