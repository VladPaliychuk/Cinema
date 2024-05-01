using AutoMapper;
using User.BLL.DTOs;
using User.BLL.Services.Interfaces;
using User.DAL.Exceptions;
using User.DAL.Repositories.Interfaces;

namespace User.BLL.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    
    public UserService(
        IMapper mapper,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    public async Task<bool> Register(UserDto userDto)
    {
        // Перевірка наявності користувача з таким же ім'ям користувача або електронною поштою
        var existingUser = await _userRepository.GetUserByUsernameOrEmail(userDto.Username, userDto.Email);
        if (existingUser != null)
        {
            return false;
        }

        var user = _mapper.Map<DAL.Entities.User>(userDto);
        await _userRepository.Create(user);
        return true;
    }

    public async Task<bool> Login(LoginDto loginDto)
    {
        var user = await _userRepository.GetByUsername(loginDto.Username);
        if (user == null)
        {
            throw new EntityNotFoundException($"User with username: {loginDto.Username} not found.");
        }

        if (!user.Password.Equals(loginDto.Password))
        {
            throw new Exception("Invalid password.");
        }

        return true;
    }
}