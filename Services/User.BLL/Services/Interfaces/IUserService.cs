using User.BLL.DTOs;

namespace User.BLL.Services.Interfaces;

public interface IUserService
{
    Task<bool> Register(UserDto userDto);
    Task<bool> Login(LoginDto loginDto);
}