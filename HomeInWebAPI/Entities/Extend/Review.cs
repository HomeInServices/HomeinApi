

namespace HomeInWebAPI.Entities
{

    public partial class Review
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
