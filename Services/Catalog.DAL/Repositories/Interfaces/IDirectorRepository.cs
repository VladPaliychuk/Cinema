using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces;

public interface IDirectorRepository
{
    Task<Director> GetById(Guid id);
    Task<IEnumerable<Director>> GetAll();
    Task<Director> GetByName (string firstName, string lastName);
    Task Create(Director director);
    Task Update(Director director);
    Task Delete(Guid id);
}