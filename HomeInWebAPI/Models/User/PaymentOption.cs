using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models.User
{ 
    public class PaymentOption
    {
       
        public string facebookid { get; set; }
        public string paymentType { get; set; }
        public string billingAddress { get; set; }
    }
}