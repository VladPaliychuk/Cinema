namespace UserCard.API.Repositories.Interfaces
{
    public interface ICardsRepository
    {
        Task<IEnumerable<Entities.UserCard>> GetCards();
        Task<IEnumerable<Entities.UserCard>> GetShortCards();
        Task<Entities.UserCard> GetCardById(Guid id);
        Task<Entities.UserCard> GetCardByUsername(string username);
        Task CreateCard(Entities.UserCard card);
        Task UpdateCard(Entities.UserCard card);
        Task DeleteCard(Guid id);
    }
}