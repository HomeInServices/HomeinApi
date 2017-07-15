
namespace HomeInWebAPI.Entities
{
    
    public partial class WorkerSkill
    {
        public bool ShouldSerializePerson()
            {
                return false;
            }
		public bool ShouldSerializeSkill()
            {
                return false;
            }
    }
}
