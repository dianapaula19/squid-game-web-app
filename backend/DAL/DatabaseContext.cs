using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace backend.DAL {
    public class DatabaseContext : IdentityDbContext {

        public virtual DbSet<ApplicationUser> ApplicationUsers {get; set;}
        public virtual DbSet<VIP> VIPS {get; set;}
        public virtual DbSet<Todo> Todos {get; set;}
        public virtual DbSet<Player> Players {get; set;}
        public virtual DbSet<Guard> Guards {get; set;}

        public virtual DbSet<ApplicationUserVIP> ApplicationUserVIPs {get; set;}
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {   
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUserVIP>()
                .HasKey(av => new { av.ApplicationUserId, av.VIPId });  
            modelBuilder.Entity<ApplicationUserVIP>()
                .HasOne(av => av.ApplicationUser)
                .WithMany(a => a.ApplicationUserVIPs)
                .HasForeignKey(av => av.ApplicationUserId);  
            modelBuilder.Entity<ApplicationUserVIP>()
                .HasOne(av => av.VIP)
                .WithMany(v => v.ApplicationUserVIPs)
                .HasForeignKey(av => av.VIPId);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(a => a.Todos)
                .WithOne(t => t.ApplicationUser);
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.PlayerInfo)
                .WithOne(p => p.ApplicationUser)
                .HasForeignKey<Player>(p => p.ApplicationUserForeignKey);
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.GuardInfo)
                .WithOne(g => g.ApplicationUser)
                .HasForeignKey<Guard>(g => g.ApplicationUserForeignKey);
            
        }
    }

}