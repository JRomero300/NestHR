using CsvHelper.Configuration;
using NestHR.Model;

public class EmployeeMap : ClassMap<Employee>
{
    public EmployeeMap()
    {
        Map(m => m.FirstName); // Map FirstName
        Map(m => m.LastName);  // Map LastName
        Map(m => m.Email);     // Map Email
        Map(m => m.Salary);    // Map Salary
        Map(m => m.EmployeeId).Ignore(); // Ignore EmployeeId
    }
}