using Domain;
using DomainServices.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class MealPackageRepository(MealSaverEFDBContext context) : IMealPackageRepository
{
    public IQueryable<MealPackage> GetAllMealPackages()
    {
        return context.MealPackages.Include(c => c.Canteen).Include(p => p.Products);
    }

    public MealPackage GetMealPackageById(Guid id)
    {
        return context.MealPackages.Include(c => c.Canteen).Include(p => p.Products).SingleOrDefault(mealPackage => mealPackage.Id == id);
    }

    public IQueryable<MealPackage> GetAllUnReservedMealPackages()
    {
        return context.MealPackages.Where(m => m.ReservedById == null).Include(m => m.Products).Include(c => c.Canteen);
    }

    public IQueryable<MealPackage> GetAllReservedMealPackages()
    {
        return context.MealPackages.Where(m => m.ReservedById != null).Include(m => m.Products).Include(s => s.ReservedBy);
    }

    public IQueryable<MealPackage> GetAllPackagesFromStudent(Guid studentId)
    {
        return context.MealPackages.Where(m => m.ReservedById == studentId).Include(c => c.Canteen).Include(p => p.Products);
    }

    public IQueryable<MealPackage> GetAllCanteenPackages(CanteenEmployee canteenEmployee)
    {
        return context.MealPackages.Include(c => c.Canteen).Include(p => p.Products)
            .Where(c => c.Canteen.Location == canteenEmployee.Canteen.Location)
            .OrderBy(m => m.PickupTimeTill);
    }

    public async Task<bool> CreateMealPackage(MealPackage mealPackage)
    {
        context.MealPackages.Add(mealPackage);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateMealPackage(MealPackage mealPackage)
    {
        context.Update(mealPackage);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteMealPackage(MealPackage mealPackage)
    {
        var mp = GetMealPackageById(mealPackage.Id);
        context.MealPackages.Remove(mp);
        return await context.SaveChangesAsync() > 0;

    }
}