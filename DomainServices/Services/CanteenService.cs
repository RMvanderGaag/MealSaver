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
        
        if (await mealPackageRepository.CreateMealPackage(mealPackage)) return true;
        return false;

    }

    public Task<bool> UpdateMealPackage(Guid mealPackageId, MealPackage mealPackage, IEnumerable<Guid> productIds)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMealPackage(Guid mealPackageId)
    {
        throw new NotImplementedException();
    }
}