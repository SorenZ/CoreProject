namespace Mobin.CoreProject.Core.Entities
{
    public class UserRole
    {
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}