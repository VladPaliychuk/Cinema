using Catalog.DAL.Entities;
using Catalog.DAL.Entities.DTOs;

namespace Catalog.BLL.Services.Interfaces;

public interface ICatalogService
{
    Task CreateAllRelations(ProductDetails productDetails);
    Task CreateProductActorRelation(string productName, string actorName);
    Task<ProductDetails> GetProductDetails(string productName);
    Task<IEnumerable<Product>> GetProductsByActorName(string actorName);
}