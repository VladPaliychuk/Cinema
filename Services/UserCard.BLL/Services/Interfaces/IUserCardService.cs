using UserCard.BLL.DTOs;

namespace UserCard.BLL.Services.Interfaces;

public interface IUserCardService
{
    Task<UserCardDto> GetUserCard(string userName);
    Task<UserCardDto> GetUserCardByUsername(string username);
    Task<IEnumerable<UserCardDto>> GetAllUserCards();
    Task<IEnumerable<UserCardDto>> GetUserCardsByCountry(string country);
    Task<IEnumerable<UserCardDto>> GetUserCardsByState(string state);
    
    Task<UserCardDto> UpdateUserCard(UserCardDto userCardDto);
    Task<UserCardDto> CreateUserCard(UserCardDto userCardDto);
    Task<bool> DeleteUserCard(string userName);
}