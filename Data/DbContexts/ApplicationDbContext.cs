using BackendTestTask.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendTestTask.Data.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Node>()
                .HasOne(n => n.Tree)
                .WithMany(t => t.Nodes)
                .HasForeignKey(n => n.TreeID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Node>()
                .HasOne(n => n.ParentNode)
                .WithMany(n => n.ChildrenNodes)
                .HasForeignKey(n => n.ParentNodeID)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Node> Node { get; set; }
        public DbSet<Tree> Tree { get; set; }
    }
}
