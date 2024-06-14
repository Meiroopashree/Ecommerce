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
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

         modelBuilder.Entity<Book>()
                .HasOne(b => b.Student)
                .WithMany(lc => lc.Courses)
                .HasForeignKey(b => b.StudentId);

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    StudentNumber = "ST-12345",
                    Name = "Alice Johnson",
                    EnrollmentDate = new DateTime(2022, 9, 1),
                    CourseId = 1
                },
                new Student
                {
                    Id = 2,
                    StudentNumber = "ST-54321",
                    Name = "Bob Brown",
                    EnrollmentDate = new DateTime(2021, 9, 1),
                    CourseId = 2
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}


