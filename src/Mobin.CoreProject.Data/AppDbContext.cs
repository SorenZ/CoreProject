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
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(b =>
            {
                b.Ignore(c => c.Id);
                b.HasKey(c => new {c.UserName, c.RoleName});
                b.Property(p => p.UserName).HasMaxLength(50);
                b.Property(p => p.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(b =>
            {
                b.Ignore(c => c.Id);
                b.HasKey(c => c.UserName);
                b.Property(p => p.UserName).HasMaxLength(50);
            });
                

            base.OnModelCreating(modelBuilder);
        }
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
