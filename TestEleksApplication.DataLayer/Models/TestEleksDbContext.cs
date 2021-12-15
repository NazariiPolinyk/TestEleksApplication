using Microsoft.EntityFrameworkCore;

namespace TestEleksApplication.DataLayer.Models
{
    public class TestEleksDbContext : DbContext
    {
        public TestEleksDbContext()
        {
        }
        public TestEleksDbContext(DbContextOptions<TestEleksDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity => 
            {
                entity.ToTable("Student");

                entity.Property(e => e.FirstName).IsRequired()
                                                 .HasMaxLength(20);
                entity.Property(e => e.LastName).IsRequired()
                                                .HasMaxLength(20);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Password).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Login).IsRequired().HasMaxLength(20);
            });
        }
    }
}
