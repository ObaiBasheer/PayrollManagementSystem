using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayrollManagementSystem.Models;

namespace PayrollManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalaryList> SalaryLists { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<SalaryRequest> SalaryRequests { get; set; }
        public DbSet<SalaryRequestItem> SalaryRequestItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Employee Seed data
            modelBuilder.Entity<Models.Employee>().HasData(
                new Models.Employee { Id = 1, Name = "John Doe", Salary = 60000m },
                new Models.Employee { Id = 2, Name = "Jane Smith", Salary = 75000m },
                new Models.Employee { Id = 3, Name = "Michael Johnson", Salary = 50000m },
                new Models.Employee { Id = 4, Name = "Emily Davis", Salary = 65000m },
                new Models.Employee { Id = 5, Name = "David Brown", Salary = 72000m }
            );
        }
    }
}
