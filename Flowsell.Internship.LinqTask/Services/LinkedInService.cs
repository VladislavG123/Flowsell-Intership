using Bogus;

namespace Flowsell.Internship.LinqTask.Services;

public interface ILinkedInService
{
    string GetSpecialization(Guid userId, Guid companyId);
}

public class LinkedInService : ILinkedInService
{
    public string GetSpecialization(Guid userId, Guid companyId)
    {
        var faker = new Faker();
        return faker.Name.JobTitle();
    }
}