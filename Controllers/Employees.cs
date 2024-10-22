using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestHR.Data;
using NestHR.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NestHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync(); // Asynchronously fetch the list of employees
            return Ok(employees); // Return the list as a 200 OK response with the employees in JSON format
        }
    }
}

