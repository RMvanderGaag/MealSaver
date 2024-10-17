using Domain;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace MealSaverAPI.GraphQL;

public class MealPackageQuery
{
    [UseProjection]
    public IQueryable<MealPackage> GetAllMealPackages([Service]MealSaverEFDBContext context)
    {
        return context.MealPackages.Include(p => p.Products).Include(c => c.Canteen);
    }
}