using Flowsell.Internship.LinqTask.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Flowsell.Internship.LinqTask.Tests;

public class EmployeeServiceTests : object
{
    private Mock<ILinkedInService> _linkedInService;
    private readonly ApplicationContext _applicationContext;

    public EmployeeServiceTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseInMemoryDatabase(GetType().Name);

        _applicationContext = new ApplicationContext(builder.Options);

        _linkedInService = new Mock<ILinkedInService>();
    }

    [Fact]
    public async Task Add_ShouldAddNewEmployee_PositiveTest()
    {
        // Arrange
        var specialization = "Faceless person";
        _linkedInService
            .Setup(x => x.GetSpecialization(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .Returns(specialization);

        var employeeService = new EmployeeService(
            _applicationContext,
            _linkedInService.Object);

        var organisation = new Organisation
        {
            Name = "Braavos",
        };
        var person = new Person
        {
            Name = "Jaqen H'gar"
        };
        _applicationContext.People.Add(person);
        _applicationContext.Organisations.Add(organisation);
        await _applicationContext.SaveChangesAsync();

        // Act
        var employeeId = await employeeService.Add(organisation, person);
        var actual = await _applicationContext.Employees.FirstOrDefaultAsync(x => x.Id.Equals(employeeId));

        // Assert
        Assert.NotNull(actual);
        Assert.Equal(
            specialization, 
            actual.Specialization);
    }

    [Fact]
    public async Task GetByPersonId_ShouldReturnPerson()
    {
        // Arrange
        var employeeService = new EmployeeService(
            _applicationContext,
            _linkedInService.Object);
        
        var organisation = new Organisation
        {
            Name = "Braavos",
        };
        var person1 = new Person
        {
            Name = "Jaqen H'gar"
        };
        _applicationContext.People.Add(person1);
        
        var person2 = new Person
        {
            Name = "Aria Stark"
        };
        _applicationContext.People.Add(person2);
        
        _applicationContext.Organisations.Add(organisation);


        var employees = new Employee[]
        {
            new()
            {
                Id = default,
                Specialization = "sp1",
                Person = person1,
                Organisation = organisation
            },
            new()
            {
                Id = default,
                Specialization = "sp2",
                Person = person2,
                Organisation = organisation
            },
        };
        _applicationContext.Employees.AddRange(employees);
        await _applicationContext.SaveChangesAsync();
        
        // Act
        var actual = await employeeService.GetByPersonId(person1.Id);
        
        // Assert
        var expected = new List<Employee>
        {
            employees[0]
        };
        actual.Should().BeEquivalentTo(expected);
    }
}