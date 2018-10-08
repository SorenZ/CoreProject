using System.Collections.Generic;
using Mobin.CoreProject.Core.ViewModels.Leaf;

namespace Mobin.CoreProject.Core.ViewModels.Forest
{
    public class ForestEditVM
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<LeafEditVM> Leafs { get; set; }
    }
}
