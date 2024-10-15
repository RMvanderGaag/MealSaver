using Domain;
using DomainServices.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Canteen;

public class CanteenMealPackagesModel(IMealPackageRepository mealPackageRepository, UserManager<IdentityUser> userManager, ICanteenEmployeeRepository canteenEmployeeRepository) : PageModel
{
    public IQueryable<Domain.MealPackage> MealPackages { get; set; }
    
    public async Task<IActionResult> OnGet()
    {
        return await OnGetCurrentCanteenPackages();
    }

    public async Task<IActionResult> OnGetCurrentCanteenPackages()
    {
        MealPackages = mealPackageRepository.GetAllCanteenPackages(GetCanteenEmployee().Result).OrderBy(m => m.PickupTimeTill);
        return Page();
    }

    public async Task<IActionResult> OnGetAllCanteenPackages()
    {
        MealPackages = mealPackageRepository.GetAllMealPackages().OrderBy(m => m.PickupTimeTill);;
        return Page();
    }
    
    private async Task<CanteenEmployee> GetCanteenEmployee()
    {
        var userId = userManager.GetUserId(HttpContext.User);
        var user = await userManager.FindByIdAsync(userId);
        return canteenEmployeeRepository.GetCanteenEmployeeByEmail(user.Email);
    }
}