using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models
{
    
    public class UserModel
    {
        public string _id { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public Address[] address { get; set; }
        public Homeinformation[] homeInformation { get; set; }
        public string zipCode { get; set; }
        public Rating[] rating { get; set; }
        public Review[] review { get; set; }
        public Phone[] phone { get; set; }
        public Payment[] payment { get; set; }
    }

    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
    }

    public class Homeinformation
    {
        public string LivingRooms { get; set; }
        public string BedRooms { get; set; }
        public string BathRooms { get; set; }
        public string Kitchen { get; set; }
        public string notes { get; set; }
    }

    public class Rating
    {
        public string worker_id { get; set; }
        public string rating { get; set; }
    }

    public class Review
    {
        public string worker_id { get; set; }
        public string comment { get; set; }
    }

    public class Phone
    {
        public string cell { get; set; }
        public string homePhone { get; set; }
    }

    public class Payment
    {
        public string type { get; set; }
        public string amount { get; set; }
        public string account_id { get; set; }
        public string approved_token { get; set; }
    }

}