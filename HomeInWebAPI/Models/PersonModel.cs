using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Models
{
    public class PersonModel
    {
        public PersonModel()
        {
            Id = "";
            Name = "";
            Email = "";
            Gender = "";
            Picture = "";

        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }
    }
}