using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using NestHR.Data;
using NestHR.Model;
using System;
using System.Globalization;

namespace CsvToDatabaseApi.Data
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!_context.Employees.Any())
            {
                // Path to the CSV file
                var csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Seed", "testProd.csv");

                if (File.Exists(csvFilePath))
                {
                    // Open the CSV file and read records
                    using (var reader = new StreamReader(csvFilePath))
                    {
                        // Configure CsvHelper with the correct CsvConfiguration
                        var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            // Add any necessary configuration options here
                            MissingFieldFound = null // Prevent errors for missing fields
                        };

                        using (var csv = new CsvReader(reader, csvConfig))
                        {
                            var employees = csv.GetRecords<Employee>().ToList();
                            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Employees ON");

                            _context.Employees.AddRange(employees);

                            await _context.SaveChangesAsync();

                            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Employees OFF");
                        }
                    }
                }
                else
                {
                    throw new FileNotFoundException("CSV file not found.");
                }
            }
        }
    }
}
