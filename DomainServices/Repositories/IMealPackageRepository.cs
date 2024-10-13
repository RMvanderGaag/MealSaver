using Domain;

namespace DomainServices.Repositories;

public interface IMealPackageRepository
{
    IQueryable<MealPackage> GetAllMealPackages();
    MealPackage GetMealPackageById(Guid id);
    IQueryable<MealPackage> GetAllUnReservedMealPackages();
    IQueryable<MealPackage> GetAllReservedMealPackages();
    IQueryable<MealPackage> GetAllPackagesFromStudent(Guid studentId);
    IQueryable<MealPackage> GetAllCanteenPackages(CanteenEmployee canteenEmployee);
    
    Task<bool> CreateMealPackage(MealPackage mealPackage);
    Task<bool> UpdateMealPackage(MealPackage mealPackage);
    Task<bool> DeleteMealPackage(MealPackage mealPackage);
}