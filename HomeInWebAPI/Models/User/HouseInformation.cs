using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models.User
{
    public class HouseInformation
    {
        public string facebookid { get; set; }
        public double LivingRooms { get; set; }
        public double BedRooms { get; set; }
        public double Kitchen { get; set; }
        public double SquareFootSize { get; set; }
    }
}