

namespace HomeInWebAPI.Entities
{
    
    public partial class PaymentProfile
    {
        public bool ShouldSerializePerson()
            {
                return false;
            }
    }
}
