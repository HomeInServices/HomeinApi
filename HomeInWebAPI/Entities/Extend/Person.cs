
namespace HomeInWebAPI.Entities
{
    public partial class Person
    {
            public bool ShouldSerializeAddresses()
            {
                return false;
            }
            public bool ShouldSerializeAddressInformations()
            {
                return false;
            }

            public bool ShouldSerializeComments()
            {
                return false;
            }
            public bool ShouldSerializeComments1()
            {
                return false;
            }
            public bool ShouldSerializeEmployers()
            {
                return false;
            }
            public bool ShouldSerializeEmployers1()
            {
                return false;
            }

            public bool ShouldSerializeLastHiredBies()
            {
            return false;
            }

            public bool ShouldSerializePaymentProfiles()
            {
                return false;
            }
            public bool ShouldSerializeWorkerExperiences()
            {
                return false;
            }
            public bool ShouldSerializeRatings()
            {
                return false;
            }
            public bool ShouldSerializeRatings1()
            {
                return false;
            }

            public bool ShouldSerializePersonRoles()
            {
                return false;
            }
            public bool ShouldSerializeWorkerSchedules()
            {
                return false;
            }

            public bool ShouldSerializeWorkerSchedules1()
            {
                return false;
            }
            public bool ShouldSerializeWorkerSkills()
            {
                return false;
            }
            public bool ShouldSerializeTransactions()
            {
                return false;
            }

            public bool ShouldSerializeTransactions1()
            {
                return false;
            }
            public bool ShouldSerializeWorkerAvailabilities()
            {
                return false;
            }

    }
}