using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(Guid id);
    Task<IEnumerable<Product>> GetProductByName(string name);
    Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);
}