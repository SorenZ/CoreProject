using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mobin.CoreProject.Core.SSOT
{
    /// <summary>
    /// [provides permissions of system]
    /// </summary>
    public enum Permissions 
    {
        [Display(Name = "داشبورد")]
        Dashboard,

        [Display(Name = "مدیریت ماژول درخت")]
        Forest,

        [Display(Name = "مدیریت کاربران و نقش‌ها")]
        UserAndRoles,
    }
}
