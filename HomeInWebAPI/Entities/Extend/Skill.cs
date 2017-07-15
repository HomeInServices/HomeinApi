
namespace HomeInWebAPI.Entities
{
    public partial class Skill
    {
		
        public bool ShouldSerializeWorkerSkills()
        {
            return false;
        }
        public bool ShouldSerializeRatings()
        {
            return false;
        }
    }
}
