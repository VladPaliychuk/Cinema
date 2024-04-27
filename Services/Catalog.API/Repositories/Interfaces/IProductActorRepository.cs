using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface IProductActorRepository
{
    Task<IEnumerable<ProductActor>> GetAll();
    Task<IEnumerable<ProductActor>> GetByProductId(string productId);
    Task<IEnumerable<ProductActor>> GetByActorId(string actorId);
    
    
    Task Create(ProductActor productActor);
    Task Update(ProductActor productActor);
    Task Delete(Guid id);
}