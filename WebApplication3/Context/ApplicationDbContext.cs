using Microsoft.EntityFrameworkCore;
using WebApplication3.Comman.Models;
using WebApplication3.Entity;

namespace WebApplication3.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<InvoiceHeader> InvoiceHeaders { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users{ get; set; }









        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
       .HasIndex(u => u.Email)
       .IsUnique();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);



        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<CreatedUser>().Where(e => e.State == EntityState.Added))
            {
                entry.Entity.CreateDateAndTime = DateTime.Now;

            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

