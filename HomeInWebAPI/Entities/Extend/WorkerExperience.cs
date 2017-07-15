using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeInWebAPI.Entities.Extend
{
    public partial class WorkerExperience
    {
        public bool ShouldSerializePerson()
        {
            return false;
        }
    }
}