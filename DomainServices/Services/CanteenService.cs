using Domain;
using DomainServices.Repositories;
using DomainServices.Services.Interface;

namespace DomainServices.Services;

public class CanteenService(IMealPackageRepository mealPackageRepository, IProductRepository productRepository) : ICanteenService
{
    public async Task<bool> AddMealPackage(Guid mealPackageId, MealPackage mealPackage, IEnumerable<Guid> productIds)
    {
        var products = productIds.Select(productRepository.GetProductById).ToList();
        mealPackage.Products = products;

        if (products.Any(p => p.ContainsAlcohol)) mealPackage.Is18Plus = true;
        
        return await mealPackageRepository.CreateMealPackage(mealPackage);
    }

    public async Task<string> UpdateMealPackage(Guid mealPackageId, MealPackage mealPackage, IEnumerable<Guid> productIds)
    {        
        var products = productIds.Select(productRepository.GetProductById).ToList();
        mealPackage.Products = products;

        if (products.Any(p => p.ContainsAlcohol)) mealPackage.Is18Plus = true;
        
        var deleteResult = await DeleteMealPackage(mealPackageId);
        
        if (deleteResult != "Meal package deleted successfully")
        {
            return deleteResult;
        }
        
        if (await mealPackageRepository.CreateMealPackage(mealPackage))
        {
            return "Meal package updated successfully";
        }

        return "Something went wrong while updating the meal package";
    }

    public async Task<string> DeleteMealPackage(Guid mealPackageId)
    {
        var mealPackage = mealPackageRepository.GetMealPackageById(mealPackageId);

        if (mealPackage != null)
        {
            if (mealPackage.ReservedBy == null)
            {
                if(await mealPackageRepository.DeleteMealPackage(mealPackage))
                {
                    return "Meal package deleted successfully";
                }
            }
            else
            {
                return "Meal package is reserved by a student";
            }
        }
        
        return "Meal package not found";
        
        
    }
}