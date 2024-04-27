using Catalog.Entities;

namespace Catalog.API.Services.Interfaces.Interfaces;

public interface ICatalogService
{
    Task CreateProductActorRelation(string productName, string actorName);
    Task<IEnumerable<Product>> GetProductsByActorName(string actorName);
}