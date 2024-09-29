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
            return "Meal package or student not found";
        }

        if (mealPackage.ReservedBy != null)
        {
            return "Meal package is already reserved";
        }

        if(mealPackage.Is18Plus && student.Age < 18)
        {
            return "Student is not 18+";
        }

        mealPackage.ReservedBy = student;
        await mealPackageRepository.UpdateMealPackage(mealPackage);

        return "Meal package reserved successfully";
        
    }
}