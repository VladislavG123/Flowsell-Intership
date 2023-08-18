namespace Flowsell.Internship.LinqTask;

public class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

public class Person : Entity
{
    public string Name { get; set; }
    public virtual List<Person> Friends { get; set; } = new();
}

public class Organisation : Entity
{
    public string Name { get; set; }
    public virtual List<Employee> Employees { get; set; } = new();
}

public class Employee : Entity
{
    public string Specialization { get; set; }
    public virtual Person Person { get; set; }
    public virtual Organisation Organisation { get; set; } = new();
}
