using System.Diagnostics;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using CsvReaderDotNet.Data;
using CsvReaderDotNet.Models;
using CsvReaderDotNet.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CsvHelper;

namespace CsvReaderDotNet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "Please upload a valid CSV file.");
                return View("Index");
            }

            using (var stream = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true
                };


                var csv = new CsvReader(stream, config);
                var records = csv.GetRecords<Employee>();
                foreach (var record in records)
                {
                    var sql = @"INSERT INTO Employees (PIN, Name, Department, Role, DOB, Address) 
                                VALUES (@PIN, @Name, @Department, @Role, @DOB, @Address)";
                    await _context.Database.ExecuteSqlRawAsync(sql,
                        new[]
                        {
                            new SqlParameter("@PIN", record.PIN),
                            new SqlParameter("@Name", record.Name),
                            new SqlParameter("@Department", record.Department),
                            new SqlParameter("@Role", record.Role),
                            new SqlParameter("@DOB", record.DOB),
                            new SqlParameter("@Address", record.Address),
                        });
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowData()
        {
            //var sql = "SELECT * FROM Employees";
            //var employee = await _context.Employees.FromSqlRaw(sql).ToListAsync();

            var employee = await _context.Employees.ToListAsync();
            return View(employee);
        }
    


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
