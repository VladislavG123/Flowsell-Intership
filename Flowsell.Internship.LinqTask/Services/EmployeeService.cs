using Microsoft.EntityFrameworkCore;

namespace Flowsell.Internship.LinqTask.Services;

public class EmployeeService
{
    private readonly ApplicationContext _context;
    private readonly ILinkedInService _linkedInService;

    public EmployeeService(ApplicationContext context, ILinkedInService linkedInService)
    {
        _context = context;
        _linkedInService = linkedInService;
    }

    public async Task<Guid> Add(Organisation organisation, Person person)
    {
        var employee = new Employee
        {
            Specialization = _linkedInService
                .GetSpecialization(person.Id, organisation.Id),
            Person = person,
            Organisation = organisation
        };
        
        _context.Employees.Add(employee);

        await _context.SaveChangesAsync();
        
        return employee.Id;
    }
    
    public async Task<List<Employee>> GetByPersonId(Guid personId)
    {
        var employee = await _context.Employees
            .FirstOrDefaultAsync(x => x.Person.Id.Equals(personId));

        if (employee is null)
        {
            throw new ArgumentException();
        }

        return new List<Employee>
        {
            employee
        };
    }
}