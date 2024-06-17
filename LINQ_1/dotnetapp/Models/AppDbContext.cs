using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Enrollment)
                .WithMany(e => e.Courses)
                .HasForeignKey(c => c.EnrollmentId);

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    Id = 1,
                    StudentName = "John Doe",
                    EnrollmentDate = new DateTime(2024, 6, 15)
                },
                new Enrollment
                {
                    Id = 2,
                    StudentName = "Jane Smith",
                    EnrollmentDate = new DateTime(2023, 8, 20)
                }
            );

            // Other configurations

            base.OnModelCreating(modelBuilder);
        }
    }
}
