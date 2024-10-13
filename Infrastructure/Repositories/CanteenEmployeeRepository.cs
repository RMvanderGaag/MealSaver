using Domain;
using DomainServices.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.EntityFramework;

public class CanteenEmployeeRepository(MealSaverEFDBContext context) : ICanteenEmployeeRepository
{

    public CanteenEmployee GetCanteenEmployeeByEmail(string email)
    {
        throw new NotImplementedException();
    }
}