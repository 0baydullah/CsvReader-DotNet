using System.ComponentModel.DataAnnotations;

namespace CsvReaderDotNet.Models.Entities
{
    public class Employee
    {
        [Key]
        public string PIN { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
    }
}
