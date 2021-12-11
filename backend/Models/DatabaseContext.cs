using Microsoft.EntityFrameworkCore;

namespace backend.Models {
    public class DatabaseContext : DbContext {
        public DatabaseContext (DbContextOptions<DatabaseContext> options) : base (options) { }
        public DbSet<User> Users { get; set; }
        
        public DbSet<FrontMan> FrontMen {get; set;}

        public DbSet<VIP> VIPS {get; set;}

        public DbSet<Task> Tasks {get; set;}
    }

}