using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces;

public interface IProductActorRepository
{
    Task<IEnumerable<ProductActor>> GetAll();
    Task<IEnumerable<ProductActor>> GetByProductId(Guid productId);
    Task<IEnumerable<ProductActor>> GetByActorId(Guid actorId);
    Task<ProductActor> GetByProductIdAndActorId(Guid productId, Guid actorId);
    
    
    Task Create(ProductActor productActor);
    Task Update(ProductActor productActor);
    Task Delete(Guid productId, Guid actorId);
}