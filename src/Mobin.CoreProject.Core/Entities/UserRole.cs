using System;
using Alamut.Data.Entity;

namespace Mobin.CoreProject.Core.Entities
{
    public class UserRole  : IEntity
    {
        [Obsolete("use {UserName, RoleId} as primary key")]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}