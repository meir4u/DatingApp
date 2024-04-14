using DatingApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLike>()
                .HasKey(k => new { k.SourceUserId, k.TargetUserId });
            modelBuilder.Entity<UserLike>()
                .HasOne(k => k.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s=>s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLike>()
                .HasOne(k => k.TargetUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.TargetUserId)
                .OnDelete(DeleteBehavior.Cascade); //in sql server one must be DeleteBehavior.Noaction

            modelBuilder.Entity<Message>()
                .HasOne(u=>u.Recipient)
                .WithMany(m=> m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(u=>u.Sender)
                .WithMany(m=>m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict); //in sql server one must be DeleteBehavior.Noaction

        }


    }
}
