
using Microsoft.EntityFrameworkCore;
using NestHR.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register DataSeeder
builder.Services.AddTransient<DataSeeder>();
// Add this line before `app.UseRouting()`
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader());
});


var app = builder.Build();

// Create a scope to resolve services from the DI container
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Ensure the database is created and seed it
    var dbContext = services.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated(); // Create database if not exists

    var dataSeeder = services.GetRequiredService<DataSeeder>();
    dataSeeder.SeedAsync(); // Seed the database with data from CSV
}


app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();