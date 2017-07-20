using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models.Worker
{
    public class SkillsPaymentOptions
    {
        public int id { get; set; }
        public int skill_id { get; set; }
        public int person_id { get; set; }
    }
}