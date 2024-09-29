using Domain;

namespace DomainServices.Repositories;

public interface IProductRepository
{
    IQueryable<Product> GetAllProducts();
    Product GetProductById(Guid id);
}