using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces;

public interface ISeatRepository
{
    Task<Seat> GetById(Guid id);
    Task<IEnumerable<Seat>> GetAll();
    Task<Seat> GetSeatWithScreeningAsync(Guid seatId);
    Task Create(Seat seat);
    Task Update(Seat seat);
    Task Delete(Guid id);
}