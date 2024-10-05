using Microsoft.EntityFrameworkCore;
using NewsPaper.Models.Entities;

namespace NewsPaper.Models.Context
{
    public class NewspaperContext:DbContext
    {
        public NewspaperContext():base() { }
        public NewspaperContext(DbContextOptions<NewspaperContext> options)
        : base(options){}

        public DbSet<Author> authors { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Post> posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasMany(a=>a.posts).WithOne(p=>p.author).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Department>().HasMany(d=>d.authors).WithOne(a=>a.department).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
