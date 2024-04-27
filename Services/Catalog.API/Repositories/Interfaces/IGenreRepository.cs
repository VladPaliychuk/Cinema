using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface IGenreRepository
{
    Task<Genre> GetById(Guid id);
    Task<IEnumerable<Genre>> GetAll();
    Task<Genre> GetByName (string name);
    Task Create(Genre genre);
    Task Update(Genre genre);
    Task Delete(Guid id);
}