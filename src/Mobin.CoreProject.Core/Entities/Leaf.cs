using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alamut.Data.Entity;

namespace Mobin.CoreProject.Core.Entities
{
    public class Leaf : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        
        public int ForestId { get; set; }

        [ForeignKey(nameof(ForestId))]
        public virtual Forest Forest { get; set; }
    }
}