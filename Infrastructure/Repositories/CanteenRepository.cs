using Domain;
using DomainServices.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.EntityFramework;

public class CanteenRepository(MealSaverEFDBContext context) : ICanteenRepository
{
    public IQueryable<Canteen> GetAllCanteens()
    {
        return context.Canteens;
    }
}