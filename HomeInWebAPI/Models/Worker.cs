using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models
{

    public class WorkerModel
    {
        public string _id { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string createdOn { get; set; }
        public string modifiedOn { get; set; }
        public string createdBy { get; set; }
        public string modifiedBy { get; set; }
        public Address[] address { get; set; }
        public string zipCode { get; set; }
        public Rating[] rating { get; set; }
        public Review[] review { get; set; }
        public Phone[] phone { get; set; }
        public Payment[] payment { get; set; }
        public string serviceExpense { get; set; }
        public Skill[] skills { get; set; }
    }

    //public class Address
    //{
    //    public string address1 { get; set; }
    //    public string address2 { get; set; }
    //}

    //public class Rating
    //{
    //    public string worker_id { get; set; }
    //    public string rating { get; set; }
    //}

    //public class Review
    //{
    //    public string worker_id { get; set; }
    //    public string comment { get; set; }
    //}

    //public class Phone
    //{
    //    public string cell { get; set; }
    //    public string homePhone { get; set; }
    //}

    //public class Payment
    //{
    //    public string type { get; set; }
    //    public string amount { get; set; }
    //    public string account_id { get; set; }
    //    public string approved_token { get; set; }
    //}

    public class Skill
    {
        public string carpetClean { get; set; }
        public string spotClean { get; set; }
    }

}