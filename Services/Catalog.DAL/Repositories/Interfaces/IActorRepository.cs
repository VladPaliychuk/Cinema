using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces;

public interface IActorRepository
{
    Task<IEnumerable<Actor>> GetAll();
    Task<Actor> GetById(Guid id);
    Task<Actor> GetByName(string firstName, string lastName);
    
    Task Create(Actor actor);
    Task Update(Actor actor);
    Task Delete(Guid id);
}