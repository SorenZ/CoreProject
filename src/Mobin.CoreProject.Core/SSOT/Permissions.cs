using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mobin.CoreProject.Core.SSOT
{
    public class AlamutClaimTypes
    {
        public const string Permission = "permission";
    }

    /// <summary>
    /// [provides permissions of system]
    /// </summary>
    public enum Permissions
    {
        ForestCreate,
        ForestEdit,
        ForestDelete,
    }
}
