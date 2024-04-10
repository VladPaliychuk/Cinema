using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(Guid id);
    Task<IEnumerable<Product>> GetProductsByName(string name);
    Task<IEnumerable<Product>> GetProductsByCategory(string categoryName);
    Task<IEnumerable<Product>> GetProductsByNameAndCategory(string name, string category);
    Task<IEnumerable<Product>> SearchProducts(string name, string category);

    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id);
}