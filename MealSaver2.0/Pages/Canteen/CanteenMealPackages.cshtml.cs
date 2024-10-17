using Domain;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Canteen;

public class CanteenMealPackagesModel(IMealPackageRepository mealPackageRepository, ICanteenService canteenService, IUserService userService) : PageModel
{
    public IQueryable<Domain.MealPackage> MealPackages { get; set; }
    
    [BindProperty]
    
    public string Message { get; set; }
    
    public async Task<IActionResult> OnGet()
    {
        Message = TempData["message"]?.ToString();
        return await OnGetCurrentCanteenPackages();
    }

    public async Task<IActionResult> OnGetCurrentCanteenPackages()
    {
        MealPackages = mealPackageRepository.GetAllCanteenPackages(userService.GetLoggedInUserInfo(User).Result as CanteenEmployee).OrderBy(m => m.PickupTimeTill);
        return Page();
    }

    public async Task<IActionResult> OnGetAllCanteenPackages()
    {
        MealPackages = mealPackageRepository.GetAllMealPackages().OrderBy(m => m.PickupTimeTill);;
        return Page();
    }

    public async Task<IActionResult> OnGetDeleteMealPackage(Guid id)
    {
        Message = await canteenService.DeleteMealPackage(id) switch
        {
            "success" => "Meal package deleted successfully",
            "not-found" => "Meal package not found",
            "already-reserved" => "Meal package has a reservation and cannot be deleted",
            _ => "Something went wrong"
        };
        TempData["message"] = Message;
        return RedirectToPage("/Canteen/CanteenMealPackages");
    }

    public async Task<IActionResult> OnGetEditMealPackage(Guid id)
    {
        if (mealPackageRepository.GetMealPackageById(id).ReservedById == null)
            return RedirectToPage("/Canteen/EditMealPackage", new { id });
        Message = "Meal package has a reservation and cannot be edited";
        TempData["message"] = Message;
        return RedirectToPage("/Canteen/CanteenMealPackages");
    }
}