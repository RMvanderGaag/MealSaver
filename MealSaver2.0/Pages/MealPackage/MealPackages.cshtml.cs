using Domain.Enums;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.MealPackage;

public class MealPackagesModel(IMealPackageRepository mealPackageRepository, IMealPackageService mealPackageService) : PageModel
{
    public IQueryable<Domain.MealPackage> AllMealPackages { get; set; }
    [BindProperty(SupportsGet = true)]
    public int SelectedLocation { get; set; } = -1;
    [BindProperty(SupportsGet = true)]
    public int SelectedMealType { get; set; } = -1;

    public void OnGet()
    {
        AllMealPackages = mealPackageService.FilterMealPackages(SelectedLocation, SelectedMealType);
    }
}