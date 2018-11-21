using Microsoft.AspNetCore.Identity;

namespace Mobin.CoreProject.Core.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole()
        {
            
        }

        public AppRole(string roleName)
            : this()
        {
            base.Name = roleName;
        }

    }
}