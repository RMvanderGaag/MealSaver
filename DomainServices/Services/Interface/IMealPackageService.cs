using Domain.Enums;

namespace DomainServices.Services.Interface;

public interface IMealPackageService
{
    Task<string> ReserveMealPackage(Guid mealPackageId, Guid studentId);
    IQueryable<Domain.MealPackage> FilterMealPackages(int location, int mealType);
}