using Domain;

namespace DomainServices.Repositories;

public interface ICanteenEmployeeRepository
{
    CanteenEmployee GetCanteenEmployeeByEmail(string email);
}