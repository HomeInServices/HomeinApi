
namespace HomeInWebAPI.Entities
{
    
    public partial class Transaction
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
