namespace Flowsell.Internship.LinqTask.Services;

public class OrganisationService
{
    private readonly ApplicationContext _context;

    public OrganisationService(ApplicationContext context, LinkedInService linkedInService)
    {
        _context = context;
    }

}