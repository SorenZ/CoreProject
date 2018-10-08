using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Mobin.CoreProject.Core.Entities;

namespace Mobin.CoreProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<Forest> Forest { get; set; }
        public DbSet<Leaf> Leaf { get; set; }
    }

    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=172.16.12.162; Database=Mobin.CoreProject;Trusted_Connection=True; MultipleActiveResultSets=true");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
