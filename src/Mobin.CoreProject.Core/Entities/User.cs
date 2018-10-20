using System;
using System.ComponentModel.DataAnnotations;
using Alamut.Data.Entity;

namespace Mobin.CoreProject.Core.Entities
{
    public class User
    {
        [Key]
        [MaxLength(50)]
        public string UserName { get; set; }
    }
}