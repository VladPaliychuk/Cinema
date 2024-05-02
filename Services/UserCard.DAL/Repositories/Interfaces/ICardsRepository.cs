namespace UserCard.DAL.Repositories.Interfaces
{
    public interface ICardsRepository
    {
        Task<IEnumerable<Entities.UserCard>> GetCards();
        Task<Entities.UserCard> GetCardById(Guid id);
        Task<Entities.UserCard> GetCardByUsername(string username);
        Task<IEnumerable<Entities.UserCard>> GetCardsByCountry(string country);
        Task<IEnumerable<Entities.UserCard>> GetCardsByState(string state);
        
        Task CreateCard(Entities.UserCard card);
        Task UpdateCard(Entities.UserCard card);
        Task DeleteCard(Guid id);
    }
}