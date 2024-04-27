using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(Guid id);
    Task<IEnumerable<Product>> GetProductsByName(string name);
    Task<Product> GetProductByName(string name);

    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);
}