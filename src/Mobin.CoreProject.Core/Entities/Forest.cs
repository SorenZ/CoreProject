using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alamut.Data.Entity;

namespace Mobin.CoreProject.Core.Entities
{
    public class Forest : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Leaf> Leafs { get; set; }
    }
}
