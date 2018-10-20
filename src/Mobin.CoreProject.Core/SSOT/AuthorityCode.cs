using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mobin.CoreProject.Core.SSOT
{
    /// <summary>
    /// [provides base roles of system]
    /// </summary>
    public enum AuthorityCode
    {
        SystemSupervisor = 0,


        #region UserManagement : مدیریت کاربران
        [Display(Name = "مدیریت کاربران : دسترسی کامل")]
        UserManagementFullAccess = 1000,

        [Display(Name = "مدیریت کاربران : حذف و اضافه")]
        UserManagementCrud = 1001,

        [Display(Name = "مدیریت کاربران : دسترسی‌ها")]
        RoleManagementFullAccess = 1002,
        #endregion
    }
}
