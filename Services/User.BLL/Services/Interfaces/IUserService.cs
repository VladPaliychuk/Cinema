namespace User.BLL.Services.Interfaces;

public interface IUserService
{
    Task UpdatePassword(string username, string newPassword);
}