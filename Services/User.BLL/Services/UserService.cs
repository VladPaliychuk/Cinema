using User.BLL.Services.Interfaces;
using User.DAL.Exceptions;
using User.DAL.Repositories.Interfaces;

namespace User.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(
        IUserRepository userRepository
        )
    {
        _userRepository = userRepository;
    }
    
    public async Task UpdatePassword(string username, string newPassword)
    {
        DAL.Entities.User user = await _userRepository.GetByUsername(username);
        
        if (user == null)
        {
            throw new EntityNotFoundException($"User with username: {username} not found.");
        }
        
        user.Password = newPassword;
        await _userRepository.Update(user);
    }
}