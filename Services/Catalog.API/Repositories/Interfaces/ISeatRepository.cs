using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface ISeatRepository
{
    Task<Seat> GetById(Guid id);
    Task<IEnumerable<Seat>> GetAll();
    Task Create(Seat seat);
    Task Update(Seat seat);
    Task Delete(Guid id);
}