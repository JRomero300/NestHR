using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NestHR.Data;
using NestHR.Model;
using System.Globalization;

public class DataSeeder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(IServiceProvider serviceProvider, ILogger<DataSeeder> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!context.Employees.Any())
            {
                try
                {
                    var csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Seed", "testProd.csv");

                    if (File.Exists(csvFilePath))
                    {
                        using (var reader = new StreamReader(csvFilePath))
                        {
                            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                            {
                                MissingFieldFound = null,
                                HeaderValidated = null,
                            };

                            using (var csv = new CsvReader(reader, csvConfig))
                            {
                                // Register the class map
                                csv.Context.RegisterClassMap<EmployeeMap>();

                                // Read records from CSV
                                var employees = csv.GetRecords<Employee>().ToList();

                                _logger.LogInformation("Number of employees loaded from CSV: {Count}", employees.Count);

                                if (employees.Any())
                                {
                                    // Add employees to the context
                                    context.Employees.AddRange(employees);
                                    await context.SaveChangesAsync();
                                }
                                else
                                {
                                    _logger.LogWarning("No employees were found in the CSV file.");
                                }
                            }
                        }
                    }
                    else
                    {
                        _logger.LogError("CSV file not found at path: {CsvFilePath}", csvFilePath);
                        throw new FileNotFoundException("CSV file not found.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while seeding the database.");
                    throw;
                }
            }
            else
            {
                _logger.LogInformation("Employees table already contains data. Skipping seeding.");
            }
        }
    }
}
