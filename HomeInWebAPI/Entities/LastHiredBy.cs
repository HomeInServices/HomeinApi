//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeInWebAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class LastHiredBy
    {
        public int id { get; set; }
        public int worker_Id { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public Nullable<decimal> user_phone { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
