using Bogus;

namespace Flowsell.Internship.LinqTask;

public class DataSeeder
{
    public List<Person> People { get; } = new();
    public List<Employee> Employees { get; } = new();
    public List<Organisation> Organisations { get; } = new();
    
    public DataSeeder()
    {
        var faker = new Faker();
        AddPeople(faker);
        AddFriends(faker);
        
        var userPartitions = MakeUserPartitions(faker);
        AddOrganisations(userPartitions, faker);
        AddEmployees(userPartitions, faker);
    }

    private void AddEmployees(List<int> userPartitions, Faker faker)
    {
        for (var i = 0; i < userPartitions.Count; i++)
        {
            var partition = userPartitions[i];

            var people = People
                .Skip(userPartitions.Take(i).Sum())
                .Take(partition)
                .ToList();

            foreach (var person in people)
            {
                Employees.Add(new Employee
                {
                    Specialization = faker.Name.JobTitle(),
                    Person = person,
                    Organisation = Organisations[i]
                });
            }
        }
    }

    private void AddOrganisations(List<int> userPartitions, Faker faker)
    {
        for (int i = 0; i < userPartitions.Count; i++)
        {
            Organisations.Add(new Organisation
            {
                Name = faker.Company.CompanyName(),
            });
        }
    }

    private static List<int> MakeUserPartitions(Faker faker)
    {
        var userPartitions = new List<int>();
        while (userPartitions.Sum() < 960)
        {
            userPartitions.Add(faker.Random.Int(1, 39));
        }

        userPartitions.Add(1000 - userPartitions.Sum());
        return userPartitions;
    }

    private void AddFriends(Faker faker)
    {
        for (var index = 0; index < People.Count; index++)
        {
            var person = People[index];
            for (int i = 0; i < faker.Random.Int(0, 50); i++)
            {
                var friendIndex = faker.Random.Int(0, 999);
                var friend = People[friendIndex];
                if (index == friendIndex || person.Friends.Any(x => x.Id.Equals(friend.Id)))
                {
                    continue;
                }

                person.Friends.Add(friend);
            }
        }
    }

    private void AddPeople(Faker faker)
    {
        for (int i = 0; i < 1000; i++)
        {
            People.Add(new Person
            {
                Name = faker.Name.FullName()
            });
        }
    }
}