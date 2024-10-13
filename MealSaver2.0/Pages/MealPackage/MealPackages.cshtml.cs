using DomainServices.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.MealPackage;

public class MealPackagesModel(IMealPackageRepository mealPackageRepository) : PageModel
{
    public IQueryable<Domain.MealPackage> AllMealPackages { get; set; }

    public void OnGet()
    {
        AllMealPackages = mealPackageRepository.GetAllUnReservedMealPackages();
    }
}