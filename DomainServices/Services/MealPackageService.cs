using DomainServices.Repositories;
using DomainServices.Services.Interface;

namespace DomainServices.Services;

public class MealPackageService(IMealPackageRepository mealPackageRepository, IStudentRepository studentRepository) : IMealPackageService
{
    public async Task<string> ReserveMealPackage(Guid mealPackageId, Guid studentId)
    {
        var mealPackage = mealPackageRepository.GetMealPackageById(mealPackageId);
        var student = studentRepository.GetStudentById(studentId);

        if (mealPackage == null || student == null)
        {
            return "not-found";
        }

        if (mealPackage.ReservedBy != null)
        {
            return "already-reserved";
        }

        if(mealPackage.Is18Plus && student.Age < 18)
        {
            return "not-eighteen";
        }
        
        if(mealPackageRepository.GetAllPackagesFromStudent(studentId).Any(s => s.PickupTimeFrom.Date == mealPackage.PickupTimeFrom.Date))
        {
            return "already-reservation-on-this-day";
        }

        mealPackage.ReservedBy = student;
        await mealPackageRepository.UpdateMealPackage(mealPackage);

        return "success";
        
    }
}