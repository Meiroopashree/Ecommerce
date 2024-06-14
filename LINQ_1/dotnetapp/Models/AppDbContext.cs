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

        public DbSet<Book> Books { get; set; }
        public DbSet<LibraryCard> LibraryCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

         modelBuilder.Entity<Book>()
                .HasOne(b => b.LibraryCard)
                .WithMany(lc => lc.Books)
                .HasForeignKey(b => b.LibraryCardId);

            modelBuilder.Entity<LibraryCard>().HasData(
                new LibraryCard
                {
                    Id = 1,
                    CardNumber = "LC-12345",
                    MemberName = "John Doe",
                    ExpiryDate = new DateTime(2025, 12, 31)
                },
                new LibraryCard
                {
                    Id = 2,
                    CardNumber = "LC-54321",
                    MemberName = "Jane Smith",
                    ExpiryDate = new DateTime(2024, 10, 15)
                }
            );

            // modelBuilder.Entity<LibraryCard>()
            //     .HasOne(lc => lc.Book)
            //     .WithOne(b => b.LibraryCard)
            //     .HasForeignKey<LibraryCard>(lc => lc.BookId); // Use the appropriate property name


            // Other configurations

            base.OnModelCreating(modelBuilder);
        }
    }
}


