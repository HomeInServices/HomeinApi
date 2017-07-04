
namespace HomeInWebAPI.Entities
{
    
    public partial class AddressType
    {
		public bool ShouldSerializeAddresses()
            {
                return false;
            }
    }
}
