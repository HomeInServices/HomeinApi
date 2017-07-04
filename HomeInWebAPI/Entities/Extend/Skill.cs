
namespace HomeInWebAPI.Entities
{
    public partial class Skill
    {
		public bool ShouldSerializeSkillsWorkers()
            {
                return false;
            }
    }
}
