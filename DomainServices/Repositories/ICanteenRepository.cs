using Domain;

namespace DomainServices.Repositories;

public interface ICanteenRepository
{
    IQueryable<Canteen> GetAllCanteens();
}