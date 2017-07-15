

namespace HomeInWebAPI.Entities
{

    public partial class Rating
    {
		public bool ShouldSerializePerson()
            {
                return false;
            }
		public bool ShouldSerializePerson1()
            {
                return false;
            }
        public bool ShouldSerializeSkill()
        {
            return false;
        }

    }
}
