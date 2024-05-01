using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces;

public interface IProductDirectorRepository
{
    Task<IEnumerable<ProductDirector>> GetAll();
    Task<IEnumerable<ProductDirector>> GetByProductId(string productId);
    Task<IEnumerable<ProductDirector>> GetByDirectorId(Guid directorId);
        
    Task Create(ProductDirector productDirector);
    Task Update(ProductDirector productDirector);
    Task Delete(Guid id);
}