namespace User.DAL.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Entities.User>> GetAll();
    Task<Entities.User> GetById(Guid id);
    Task<Entities.User> GetByEmail(string email);
    Task<Entities.User> GetByUsername(string username);
    Task<Entities.User> GetUserByUsernameOrEmail(string username, string email);
    
    Task Create(Entities.User product);
    Task Update(Entities.User product);
    Task Delete(Guid id);
    Task DeleteByUsername(string username);
}