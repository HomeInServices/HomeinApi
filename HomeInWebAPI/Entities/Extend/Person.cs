
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

            public bool ShouldSerializePaymentProfiles()
            {
                return false;
            }
            public bool ShouldSerializeReviews()
            {
                return false;
            }
            public bool ShouldSerializeReviews1()
            {
                return false;
            }

            public bool ShouldSerializePersonRoles()
            {
                return false;
            }
            public bool ShouldSerializeSchedulings()
            {
                return false;
            }

            public bool ShouldSerializeSchedulings1()
            {
                return false;
            }
            public bool ShouldSerializeSkillsWorkers()
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

    }
}