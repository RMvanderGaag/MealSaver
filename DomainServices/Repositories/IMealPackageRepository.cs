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
    
    Task<MealPackage> CreateMealPackage(MealPackage mealPackage);
    Task<MealPackage> UpdateMealPackage(MealPackage mealPackage);
    Task<MealPackage> DeleteMealPackage(MealPackage mealPackage);
}