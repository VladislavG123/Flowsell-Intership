using FluentAssertions;

namespace Flowsell.Internship.LinqTask.Tests;

public class DataSeederTests
{
    private DataSeeder _dataSeeder = new();

    [Fact]
    public void ShouldHave1000People()
    {
        Assert.Equal(
            1000,
            _dataSeeder.People.Count);
    }
    
    [Fact]
    public void ShouldHave1000Employees()
    {
        Assert.Equal(
            1000,
            _dataSeeder.Employees.Count);
    }
    
    [Fact]
    public void ShouldAllHaveOrganisationAndPerson()
    {
        foreach (var employee in _dataSeeder.Employees)
        {
            employee.Organisation.Should().NotBeNull();
            employee.Person.Should().NotBeNull();
        }
    }

}