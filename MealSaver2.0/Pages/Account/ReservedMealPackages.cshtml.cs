using DomainServices.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Account;

public class ReservedMealPackagesModel(IMealPackageRepository mealPackageRepository, UserManager<IdentityUser> userManager, IStudentRepository studentRepository) : PageModel
{
    public IQueryable<Domain.MealPackage> ReservedMealPackages { get; set; }
    
    public async Task OnGetAsync()
    {
        var userId = userManager.GetUserId(HttpContext.User);
        var user = await userManager.FindByIdAsync(userId);
        var student = studentRepository.GetStudentByEmail(user.Email);
        
        ReservedMealPackages = mealPackageRepository.GetAllPackagesFromStudent(student.Id);
    }
}