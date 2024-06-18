
using dotnetapp.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}

