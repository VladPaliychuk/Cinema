using Microsoft.EntityFrameworkCore;
using User.DAL.Data;
using User.DAL.Exceptions;
using User.DAL.Repositories.Interfaces;

namespace User.DAL.Repositories;

public class UserRepository : IUserRepository
{
    protected readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Entities.User>> GetAll()
    {
        return await _context.Users.ToListAsync()
               ?? throw new Exception($"Couldn't retrieve entities User ");
    }

    public async Task<Entities.User> GetById(Guid id)
    {
        return await _context.Users.FindAsync(id)
               ?? throw new EntityNotFoundException($"User with id {id} not found.");
    }

    public async Task<Entities.User?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
    
    public async Task<Entities.User?> GetByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }
    
    public async Task<Entities.User?> GetUserByUsernameOrEmail(string username, string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username || u.Email == email);

        return user;
    }

    
    public async Task Create(Entities.User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException($"User entity must not be null");
        }
        user.Id= Guid.NewGuid();
        _context.Set<Entities.User>().Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Entities.User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException($"User entity must not be null");
        }
        
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id) 
                     ?? throw new EntityNotFoundException($"User with id {id} not found. Can't delete.");
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteByUsername(string username)
    {
        var entity = await GetByUsername(username) 
                     ?? throw new EntityNotFoundException($"User with username {username} not found. Can't delete.");
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }
}