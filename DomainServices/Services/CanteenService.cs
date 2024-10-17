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

    public async Task<bool> UpdateMealPackage(Guid mealPackageId, MealPackage mealPackage, IEnumerable<Guid> productIds)
    {        
        var products = productIds.Select(productRepository.GetProductById).ToList();
        mealPackage.Products = products;

        if (products.Any(p => p.ContainsAlcohol)) mealPackage.Is18Plus = true;
        
        var deleteResult = await DeleteMealPackage(mealPackageId);
        
        if (deleteResult != "success")
        {
            return false;
        }
        
        return await mealPackageRepository.CreateMealPackage(mealPackage);
    }

    public async Task<string> DeleteMealPackage(Guid mealPackageId)
    {
        var mealPackage = mealPackageRepository.GetMealPackageById(mealPackageId);

        if (mealPackage != null)
        {
            if (mealPackage.ReservedById == null)
            {
                if(await mealPackageRepository.DeleteMealPackage(mealPackage))
                {
                    return "success";
                }
            }
            else
            {
                return "already-reserved";
            }
        }
        
        return "not-found";
        
        
    }
}