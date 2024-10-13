using Domain;
using DomainServices.Repositories;
using Infrastructure.Contexts;

namespace Infrastructure.EntityFramework;

public class ProductRepository(MealSaverEFDBContext context) : IProductRepository
{
    public IQueryable<Product> GetAllProducts()
    {
        return context.Products;
    }

    public Product GetProductById(Guid id)
    {
        return context.Products.SingleOrDefault(product => product.Id == id);
    }
}