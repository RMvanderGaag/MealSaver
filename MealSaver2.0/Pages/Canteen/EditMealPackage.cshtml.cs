using Domain;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using MealSaver2._0.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Canteen;

public class EditMealPackage(IMealPackageRepository mealPackageRepository, IProductRepository productRepository, ICanteenService canteenService) : PageModel
{
    [BindProperty]
    public MealPackageViewModel MealPackage { get; set; }
    public IQueryable<Product> Products { get; set; }
    
    public void OnGet(Guid id)
    {
        var mealPackage = mealPackageRepository.GetMealPackageById(id);
        Products = productRepository.GetAllProducts();
        MealPackage = new MealPackageViewModel
        {
            Id = mealPackage.Id,
            DescriptiveName = mealPackage.DescriptiveName,
            PickupTimeTill = mealPackage.PickupTimeTill,
            Price = mealPackage.Price,
            MealType = mealPackage.MealType,
            SelectedProducts = mealPackage.Products.Select(p => p.Id).ToList(),
            CanteenId = mealPackage.CanteenId
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Products = productRepository.GetAllProducts();

        var currentMealPackage = mealPackageRepository.GetMealPackageById(MealPackage.Id);
        
                
        if(MealPackage.PickupTimeTill < DateTime.Now)
        {
            ModelState.AddModelError("MealPackage.PickupTimeTill", "Pickup time must be in the future");
        }

        if (MealPackage.PickupTimeTill > DateTime.Now.AddDays(2))
        {
            ModelState.AddModelError("MealPackage.PickupTimeTill", "Pickup time must be within 2 days");
        }
        
        if(currentMealPackage.ReservedById != null)
        {
            ModelState.AddModelError("MealPackage.Id", "This meal package is already reserved");
        }

        if (!ModelState.IsValid) return Page();
        var mealPackage = new Domain.MealPackage
        {
            DescriptiveName = MealPackage.DescriptiveName,
            PickupTimeFrom = DateTime.Now,
            PickupTimeTill = MealPackage.PickupTimeTill,
            Products = null,
            Is18Plus = false,
            Price = MealPackage.Price,
            MealType = MealPackage.MealType,
            CanteenId = MealPackage.CanteenId.Value
        };
        
        
        if (!await canteenService.UpdateMealPackage(currentMealPackage.Id, mealPackage, MealPackage.SelectedProducts)) return Page();
        TempData["Message"] = "Meal package edited successfully";
        return RedirectToPage("/Canteen/CanteenMealPackages");
    }
}