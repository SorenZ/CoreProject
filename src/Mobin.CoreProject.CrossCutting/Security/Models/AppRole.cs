using Microsoft.AspNetCore.Identity;

namespace Mobin.CoreProject.CrossCutting.Security.Models
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