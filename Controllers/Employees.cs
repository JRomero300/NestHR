using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestHR.Data;

namespace NestHR.Controllers
{

    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }
    }
}
