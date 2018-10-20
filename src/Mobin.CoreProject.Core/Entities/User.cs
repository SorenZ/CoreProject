using System;
using System.ComponentModel.DataAnnotations;
using Alamut.Data.Entity;

namespace Mobin.CoreProject.Core.Entities
{
    public class User : IEntity
    {
        [Obsolete("use {UserName} as primary key")]
        public int Id { get; set; }

        [Key]
        [MaxLength(50)]
        public string UserName { get; set; }
    }
}