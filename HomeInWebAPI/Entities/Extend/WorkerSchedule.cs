
namespace HomeInWebAPI.Entities
{
    public partial class WorkerSchedule
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
