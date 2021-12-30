using Microsoft.EntityFrameworkCore;
using ProjectStoreBlazor.Server.Entities;


namespace ProjectStoreBlazor.Server.Entities
{
    public class StoreDbContext : DbContext
    {

        //public StoreDbContext(DbContextOptions<StoreDbContext> options) :base(options)
        //{

        //}

        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=ProjectStore;Trusted_Connection=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
            modelBuilder.Entity<User>()
                .Property(u=>u.Username)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Address>()
                .Property(p => p.Street)
                .IsRequired()
                .HasMaxLength(50);




        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
