using Microsoft.AspNetCore.Mvc;

namespace Flowsell.Internship.LinqTask.Controllers;

public class SeedController : ControllerBase
{
    private readonly ApplicationContext _context;

    public SeedController(ApplicationContext context)
    {
        _context = context;
    }
    
    [HttpPost("api/seed")]
    public async Task<IActionResult> Seed()
    {
        var seeder = new DataSeeder();

        _context.People.AddRange(seeder.People);
        _context.Organisations.AddRange(seeder.Organisations);
        _context.Employees.AddRange(seeder.Employees);

        await _context.SaveChangesAsync();

        return NoContent();
    }
    
}