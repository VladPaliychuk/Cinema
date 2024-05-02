using User.BLL.DTOs;

namespace User.BLL.Services.Interfaces;

public interface IAuthService
{
    Task<bool> Register(UserDto userDto);
    Task<bool> Login(LoginDto loginDto);
    
}