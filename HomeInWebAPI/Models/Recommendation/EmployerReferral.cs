using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models.Recommendation
{
    public class EmployerReferral
    {
        public string facebookid { get; set; }
        public int worker_id { get; set; }
        public Dictionary<int,int> SkillIds { get; set; }
        public string workExperienceWithEmployer { get; set; }//weeks
        public string employerReferral { get; set; }
        public decimal avgPriceChargedWithEmployer { get; set; }
    }
}