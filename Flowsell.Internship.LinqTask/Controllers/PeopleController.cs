using Microsoft.AspNetCore.Mvc;

namespace Flowsell.Internship.LinqTask.Controllers;

public class PeopleController : ControllerBase
{
    private readonly ApplicationContext _context;

    public PeopleController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet("api/people")]
    public async Task<IActionResult> GetAllPeople()
    {
        var people = _context.People.ToList();

        return Ok(people);
    }

}