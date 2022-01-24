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
        }
    }

}