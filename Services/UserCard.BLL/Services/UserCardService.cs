using AutoMapper;
using UserCard.BLL.DTOs;
using UserCard.BLL.Services.Interfaces;
using UserCard.DAL.Exceptions;
using UserCard.DAL.Repositories.Interfaces;

namespace UserCard.BLL.Services;

public class UserCardService : IUserCardService
{
    private readonly IMapper _mapper;
    private readonly ICardsRepository _cardsRepository;
    
    public UserCardService(
        IMapper mapper,
        ICardsRepository cardsRepository)
    {
        _mapper = mapper;
        _cardsRepository = cardsRepository;
    }
    
    public async Task<UserCardDto> GetUserCard(string userName)
    { 
        var userCard = await _cardsRepository.GetCardByUsername(userName);
        if (userCard == null)
        {
            throw new EntityNotFoundException($"User card with username {userName} not found");
        }

        return _mapper.Map<UserCardDto>(userCard);
    }

    public async Task<UserCardDto> GetUserCardByUsername(string username)
    {
        var userCard = await _cardsRepository.GetCardByUsername(username);
        if (userCard == null)
        {
            throw new EntityNotFoundException($"User card with username {username} not found");
        }
        
        return _mapper.Map<UserCardDto>(userCard);
    }

    public async Task<UserCardDto> UpdateUserCard(UserCardDto userCardDto)
    {
        var userCard = await _cardsRepository.GetCardByUsername(userCardDto.UserName);
        if (userCard == null)
        {
            throw new EntityNotFoundException($"User card with username {userCardDto.UserName} not found");
        }

        _mapper.Map(userCardDto, userCard);
        await _cardsRepository.UpdateCard(userCard);

        return _mapper.Map<UserCardDto>(userCard);
    }

    public async Task<UserCardDto> CreateUserCard(UserCardDto userCardDto)
    {
        var userCard = _mapper.Map<DAL.Entities.UserCard>(userCardDto);
        await _cardsRepository.CreateCard(userCard);

        return _mapper.Map<UserCardDto>(userCard);
    }
    
    public async Task<bool> DeleteUserCard(string userName)
    {
        var userCard = await _cardsRepository.GetCardByUsername(userName);
        if (userCard == null)
        {
            throw new EntityNotFoundException($"User card with username {userName} not found");
        }

        await _cardsRepository.DeleteCard(userCard.Id);
        return true;
    }

    public async Task<IEnumerable<UserCardDto>> GetAllUserCards()
    {
        var userCards = await _cardsRepository.GetCards();
        return _mapper.Map<IEnumerable<UserCardDto>>(userCards);
    }

    public async Task<IEnumerable<UserCardDto>> GetUserCardsByCountry(string country)
    {
        var userCards = await _cardsRepository.GetCardsByCountry(country);
        return _mapper.Map<IEnumerable<UserCardDto>>(userCards);
    }

    public async Task<IEnumerable<UserCardDto>> GetUserCardsByState(string state)
    {
        var userCards = await _cardsRepository.GetCardsByState(state);
        return _mapper.Map<IEnumerable<UserCardDto>>(userCards);
    }
}