using Domain;
using DomainServices.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class CanteenEmployeeRepository(MealSaverEFDBContext context) : ICanteenEmployeeRepository
{

    public CanteenEmployee GetCanteenEmployeeByEmail(string email)
    {
        return context.CanteenEmployees.Include(c => c.Canteen).SingleOrDefault(canteenEmployee => canteenEmployee.Email == email);
    }
}