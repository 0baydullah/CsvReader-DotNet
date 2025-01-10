using Microsoft.EntityFrameworkCore;
using CsvReaderDotNet.Models.Entities;

namespace CsvReaderDotNet.Data
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
