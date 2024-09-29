using Domain;

namespace DomainServices.Services.Interface;

public interface ICanteenService
{
    Task<bool> AddMealPackage(Guid mealPackageId, MealPackage mealPackage, IEnumerable<Guid> productIds);
    Task<bool> UpdateMealPackage(Guid mealPackageId, MealPackage mealPackage, IEnumerable<Guid> productIds);
    Task<bool> DeleteMealPackage(Guid mealPackageId);
}