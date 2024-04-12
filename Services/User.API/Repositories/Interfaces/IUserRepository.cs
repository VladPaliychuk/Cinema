namespace User.API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Entities.User>> GetAll();
    Task <Entities.User> GetById(Guid id);
    Task Create(Entities.User product);
    Task Update(Entities.User product);
    Task Delete(Guid id);
}