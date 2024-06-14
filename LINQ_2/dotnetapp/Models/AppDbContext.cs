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
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between Course and Student
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Ensure deletion is restricted, change it if necessary

            // Seed initial data
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    StudentNumber = "ST-12345",
                    Name = "Alice Johnson",
                    EnrollmentDate = new DateTime(2022, 9, 1),
                },
                new Student
                {
                    Id = 2,
                    StudentNumber = "ST-54321",
                    Name = "Bob Brown",
                    EnrollmentDate = new DateTime(2021, 9, 1),
                }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
