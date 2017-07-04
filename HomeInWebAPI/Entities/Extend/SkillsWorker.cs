
namespace HomeInWebAPI.Entities
{
    
    public partial class SkillsWorker
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
