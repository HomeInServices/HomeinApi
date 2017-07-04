
namespace HomeInWebAPI.Entities
{
    public partial class Role
    {
		public bool ShouldSerializePersonRoles()
            {
                return false;
            }
    }
}
