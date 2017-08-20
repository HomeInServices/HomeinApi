using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models.Worker
{
    public class SkillsPaymentOptions
    {
        
        public string facebookid { get; set; }
        public List<dynamic> SkillIds { get; set; }
        public string paymentType { get; set; }
    }
}