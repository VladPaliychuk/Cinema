using Microsoft.EntityFrameworkCore;
using UserCard.API.Data;
using UserCard.API.Exceptions;
using UserCard.API.Repositories.Interfaces;

namespace UserCard.API.Repositories;

public class CardsRepository : ICardsRepository
{
    private readonly CardsContext _context;

    public CardsRepository(CardsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Entities.UserCard>> GetCards()
    {
        return await _context.UserCards.ToListAsync()
               ?? throw new Exception($"Couldn't retrieve entities UserCards ");
    }

    public async Task<IEnumerable<Entities.UserCard>> GetShortCards()
    {
        return await _context.UserCards
                   .Select(card => new Entities.UserCard
                   {
                       Id = card.Id,
                       UserName = card.UserName,
                       Bonuses = card.Bonuses
                   })
                   .ToListAsync()
               ?? throw new Exception("Couldn't retrieve entities UserCards");
    }

    public async Task<Entities.UserCard> GetCardById(Guid id)
    {
        return await _context.UserCards.FindAsync(id)
               ?? throw new EntityNotFoundException($"UserCard with id {id} not found.");
    }

    public async Task<Entities.UserCard> GetCardByUsername(string username)
    {
        return await _context.UserCards
                   .FirstOrDefaultAsync(card => card.UserName == username)
               ?? throw new EntityNotFoundException($"UserCard with username {username} not found");
    }

    public async Task CreateCard(Entities.UserCard card)
    {
        if (card == null)
        {
            throw new ArgumentNullException($"UserCard entity must not be null");
        }

        // Перевірка наявності користувача з таким же ім'ям користувача
        var existingUser = await _context.Set<Entities.UserCard>()
            .FirstOrDefaultAsync(c => c.UserName == card.UserName);

        if (existingUser != null)
        {
            // Користувач з таким ім'ям вже існує, тому не створюємо нову картку
            throw new ArgumentException($"User with username '{card.UserName}' already exists");
        }

        card.Id = Guid.NewGuid();
        _context.Set<Entities.UserCard>().Add(card); 
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCard(Entities.UserCard card)
    {
        if (card == null)
        {
            throw new ArgumentNullException($"UserCard entity must not be null");
        }
        _context.Entry(card).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteCard(Guid id)
    {
        var entity = await GetCardById(id) 
                     ?? throw new EntityNotFoundException($"Card with id {id} not found. Can't delete.");
        _context.UserCards.Remove(entity);
        await _context.SaveChangesAsync();
    }
}