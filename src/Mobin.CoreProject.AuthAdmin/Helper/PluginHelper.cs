using System.Collections.Generic;
using Alamut.Data.Paging;

namespace Mobin.CoreProject.AuthAdmin.Helper
{
    public static class PluginHelper
    {
        public static string PersianDateTimePicker()
        {
            return @"
                <script src='/dashmix/js/plugins/MD.BootstrapPersianDateTimePicker/dist/jquery.md.bootstrap.datetimepicker.js'></script>
                <link rel='stylesheet' href='/dashmix/js/plugins/MD.BootstrapPersianDateTimePicker/dist/jquery.md.bootstrap.datetimepicker.style.css' />
            ";
        }
    }
}