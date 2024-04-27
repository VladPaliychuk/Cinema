using Catalog.Entities;

namespace Catalog.Repositories.Interfaces;

public interface IScreeningRepository
{
    Task<Screening> GetById(Guid id);
    Task<IEnumerable<Screening>> GetAll();
    Task Create(Screening screening);
    Task Update(Screening screening);
    Task Delete(Guid id);
}