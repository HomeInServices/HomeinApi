
namespace HomeInWebAPI.Entities
{
    public partial class PersonRole
    {
        public bool ShouldSerializePerson()
            {
                return false;
            }
		public bool ShouldSerializeRole()
            {
                return false;
            }
    }
}
