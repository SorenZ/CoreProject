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
        YouDoNotHaveThisRole = -1,

        SystemSupervisor = 0,


        #region UserManagement : مدیریت کاربران
        [Display(Name = "دسترسی کامل")]
        UserManagementFullAccess = 1000,

        [Display(Name = "مدیریت کاربران")]
        UserManagementManage = 1001,

        [Display(Name = "هدف از ثبت نام و دسترسی‌های اولیه")]
        UserManagementRequestReason = 1002,
        #endregion
    }
}
