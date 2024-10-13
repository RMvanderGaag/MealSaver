using DomainServices.Repositories;
using DomainServices.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.MealPackage;

public class MealPackageDetailsModel(IMealPackageRepository mealPackageRepository, IStudentRepository studentRepository, UserManager<IdentityUser> userManager, IMealPackageService mealPackageService) : PageModel
{
    
    [BindProperty]
    public Domain.MealPackage MealPackage { get; set; }
    
    [BindProperty]
    public string Message { get; set; }
    
    public void OnGet(Guid id)
    {
        Message = TempData["message"]?.ToString();
        MealPackage = mealPackageRepository.GetMealPackageById(id);
    }
    
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        var mealPackage = mealPackageRepository.GetMealPackageById(MealPackage.Id);

        var userId = userManager.GetUserId(HttpContext.User);
        var user = await userManager.FindByIdAsync(userId);
        var student = studentRepository.GetStudentByEmail(user.Email);

        Message = await mealPackageService.ReserveMealPackage(mealPackage.Id, student.Id) switch
        {
            "not-found" => "Meal package or student not found",
            "already-reserved" => "Meal package already reserved",
            "not-eighteen" => "You are not 18+",
            "success" => "Meal package successfully reserved",
            "already-reservation-on-this-day" => "You already have a reservation on this day",
            _ => "Something went wrong"
        };

        TempData["message"] = Message;
        return RedirectToPage("/MealPackage/MealPackageDetails", new { id = mealPackage.Id });
    }
}