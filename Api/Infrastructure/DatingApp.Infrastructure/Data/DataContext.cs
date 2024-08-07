﻿using DatingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<
                                                    AppUser, 
                                                    AppRole, 
                                                    int, 
                                                    IdentityUserClaim<int>, 
                                                    AppUserRole, 
                                                    IdentityUserLogin<int>, 
                                                    IdentityRoleClaim<int>, 
                                                    IdentityUserToken<int>
                                                >
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Photo> Photos{ get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

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

            modelBuilder.Entity<Photo>(e => e.Property(e => e.IsApproved).HasConversion<int>());
            modelBuilder.Entity<Photo>().HasQueryFilter(p=>p.IsApproved == true);



        }


    }
}
