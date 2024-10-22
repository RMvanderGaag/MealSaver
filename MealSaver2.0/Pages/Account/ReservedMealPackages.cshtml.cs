using Domain;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Account;

public class ReservedMealPackagesModel(IMealPackageRepository mealPackageRepository, IUserService userService) : PageModel
{
    public IQueryable<Domain.MealPackage> ReservedMealPackages { get; set; }
    
    public async Task OnGetAsync()
    {
        ReservedMealPackages = mealPackageRepository.GetAllPackagesFromStudent((userService.GetLoggedInUserInfo(User).Result as Student).Id);
    }
}