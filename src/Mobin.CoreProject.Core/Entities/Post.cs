using Alamut.Data.Entity;

namespace Mobin.CoreProject.Core.Entities
{
    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}