using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.DAL {
    public class DatabaseContext : DbContext {

        public virtual DbSet<FrontMan> FrontMen {get; set;}
        public virtual DbSet<VIP> VIPS {get; set;}
        public virtual DbSet<User> Users {get; set;}
        public virtual DbSet<Todo> Todos {get; set;}
        public virtual DbSet<Player> Players {get; set;}
        public virtual DbSet<Guard> Guards {get; set;}
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base (options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .HasDefaultValue("alive");
        }
    }

}