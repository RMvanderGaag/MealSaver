namespace DomainServices.Services.Interface;

public interface IMealPackageService
{
    Task<string> ReserveMealPackage(Guid mealPackageId, Guid studentId);
}