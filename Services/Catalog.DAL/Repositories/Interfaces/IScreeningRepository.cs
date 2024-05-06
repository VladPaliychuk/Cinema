using Catalog.DAL.Entities;

namespace Catalog.DAL.Repositories.Interfaces;

public interface IScreeningRepository
{
    Task<Screening> GetById(Guid id);
    Task<IEnumerable<Screening>> GetAll();
    Task<IEnumerable<Screening>> GetByProductId(Guid productId);
    Task<Screening> GetByDateTime(string startDate, string startTime);
    
    Task Create(Screening screening);
    Task Update(Screening screening);
    Task Delete(Guid id);
}