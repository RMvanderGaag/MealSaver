﻿using Domain;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using MealSaver2._0.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Canteen;

public class CreateMealPackageModel(IProductRepository productRepository, IUserService userService, ICanteenService canteenService) : PageModel
{
    [BindProperty]
    public MealPackageViewModel MealPackage { get; set; }
    
    public IQueryable<Product> Products { get; set; }
    
    public void OnGet()
    {
        MealPackage = new MealPackageViewModel();
        Products = productRepository.GetAllProducts();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        Products = productRepository.GetAllProducts();
        MealPackage.CanteenId = (userService.GetLoggedInUserInfo(User).Result as CanteenEmployee).CanteenId;
        
        if(MealPackage.PickupTimeTill < DateTime.Now)
        {
            ModelState.AddModelError("MealPackage.PickupTimeTill", "Pickup time must be in the future");
        }

        if (MealPackage.PickupTimeTill > DateTime.Now.AddDays(2))
        {
            ModelState.AddModelError("MealPackage.PickupTimeTill", "Pickup time must be within 2 days");
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

        if (!await canteenService.AddMealPackage(new Guid(), mealPackage, MealPackage.SelectedProducts)) return Page();
        TempData["Message"] = "Meal package created successfully";
        return RedirectToPage("/Canteen/CanteenMealPackages");

    }
}