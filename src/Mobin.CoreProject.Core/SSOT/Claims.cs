using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mobin.CoreProject.Core.SSOT
{
    /// <summary>
    /// [provides user defined claims]
    /// </summary>
    public enum Claims
    {
        [Display(Name = "کد پرسنلی")]
        EmployeeId,

        [Display(Name = "کد هتل")]
        HotelId,
    }
}
