using System;
using System.Collections.Generic;
using System.Text;
using Mobin.CoreProject.Core.DTOs.Leaf;

namespace Mobin.CoreProject.Core.DTOs.Forest
{
    public class ForestEditDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LeafEditDTO> Leafs { get; set; }

    }
}
