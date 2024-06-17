using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

         modelBuilder.Entity<Course>()
                .HasOne(b => b.Enrollment)
                .WithMany(lc => lc.Courses)
                .HasForeignKey(b => b.EnrollmentId);

            modelBuilder.Entity<Enrollment>().HasData(
                new Enrollment
                {
                    Id = 1,
                    StudentName = "John Doe",
                    EnrollmentDate = new DateTime(2025, 12, 31)
                },
                new Enrollment
                {
                    Id = 2,
                    StudentName = "Jane Smith",
                    EnrollmentDate = new DateTime(2024, 10, 15)
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}


