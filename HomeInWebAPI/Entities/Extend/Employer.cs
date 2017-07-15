using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Entities.Extend
{
    public partial class Employer
    {
        public bool ShouldSerializePerson()
        {
            return false;
        }
        public bool ShouldSerializePerson1()
        {
            return false;
        }
    }
}