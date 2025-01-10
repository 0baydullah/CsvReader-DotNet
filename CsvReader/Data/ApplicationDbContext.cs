using Microsoft.EntityFrameworkCore;
using CsvReader.Models.Entities;

namespace CsvReader.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
