using template.Models;
using Microsoft.EntityFrameworkCore;

namespace template.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);
        ////    modelBuilder.Entity<Post>()
        ////.HasOne(e => e.User);
        //    //modelBuilder.Entity<User>().HasMany(e => e.Posts); 
      
        //}
    }
}
