using Domain;
using DomainServices.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MealSaver2._0.Pages.Canteen;

public class CreateMealPackageModel(IProductRepository productRepository) : PageModel
{
    [BindProperty]
    public Domain.MealPackage MealPackage { get; set; }
    
    public IQueryable<Product> Products { get; set; }
    
    public void OnGet()
    {
        MealPackage = new Domain.MealPackage();
        Products = productRepository.GetAllProducts();
    }
    
    public IActionResult OnPost()
    {
        Products = productRepository.GetAllProducts();
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        return RedirectToPage("Index");
    }
}