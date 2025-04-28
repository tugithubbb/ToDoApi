using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        { }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<ActiveToken> ActiveTokens { get; set; }
        public DbSet<InvalidatedToken> InvalidatedTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<TodoItem>()
                .HasOne<ApplicationUser>(t => t.User)
                 .WithMany(u => u.Todos)
                 .HasForeignKey(t => t.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ActiveToken>()
                .HasOne(at => at.User)
                 .WithMany(u => u.ActiveTokens)
                 .HasForeignKey(at => at.UserId);
            builder.Entity<InvalidatedToken>()
                .HasOne(It => It.User)
                 .WithMany(u => u.InvalidatedTokens)
                 .HasForeignKey(It => It.UserId);

        }
    }
}
