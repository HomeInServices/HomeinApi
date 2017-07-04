
namespace HomeInWebAPI.Entities
{
    public partial class Address
    {
		public bool ShouldSerializeAddressType()
            {
                return false;
            }
		public bool ShouldSerializePerson()
            {
                return false;
            }
    }
}
