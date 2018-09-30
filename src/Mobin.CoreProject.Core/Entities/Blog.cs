using System;
using System.Collections.Generic;
using System.Text;
using Alamut.Data.Entity;

namespace Mobin.CoreProject.Core.Entities
{
    public class Blog : IEntity
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }
}
