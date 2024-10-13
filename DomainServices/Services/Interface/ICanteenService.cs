using Domain;

namespace DomainServices.Services.Interface;

public interface ICanteenService
{
    Task<bool> AddMealPackage(Guid mealPackageId, MealPackage mealPackage, IEnumerable<Guid> productIds);
    Task<string> UpdateMealPackage(Guid mealPackageId, MealPackage mealPackage, IEnumerable<Guid> productIds);
    Task<string> DeleteMealPackage(Guid mealPackageId);
}