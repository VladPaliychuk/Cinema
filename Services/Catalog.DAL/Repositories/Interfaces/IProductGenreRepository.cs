using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces;

public interface IProductGenreRepository
{
    Task<IEnumerable<ProductGenre>> GetAll();
    Task<IEnumerable<ProductGenre>> GetByProductId(string productId);
    Task<IEnumerable<ProductGenre>> GetByGenreId(Guid genreId);
    Task<ProductGenre> GetByProductIdAndGenreId(Guid productId, Guid genreId);
        
    Task Create(ProductGenre productGenre);
    Task Update(ProductGenre productGenre);
    Task Delete(Guid productId, Guid genreId);
}