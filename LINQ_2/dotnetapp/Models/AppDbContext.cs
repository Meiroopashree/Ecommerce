using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Introduction to Programming", Description = "Learn the basics of programming.", Duration = 40 },
                new Course { Id = 2, Title = "Advanced Databases", Description = "Deep dive into database management systems.", Duration = 60 }
            );
        }
    }
}
