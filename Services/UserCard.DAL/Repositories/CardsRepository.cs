using Microsoft.EntityFrameworkCore;
using UserCard.DAL.Data;
using UserCard.DAL.Exceptions;
using UserCard.DAL.Repositories.Interfaces;

namespace UserCard.DAL.Repositories;

public class CardsRepository : ICardsRepository
{
    private readonly CardsContext _context;

    public CardsRepository(CardsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Entities.UserCard>> GetCards()
    {
        return await _context.UserCards.ToListAsync();
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

    public async Task<IEnumerable<Entities.UserCard>> GetCardsByCountry(string country)
    {
        return await _context.UserCards
                   .Where(card => card.Country == country)
                   .ToListAsync()
               ?? throw new EntityNotFoundException($"UserCard with country {country} not found");
    }

    public async Task<IEnumerable<Entities.UserCard>> GetCardsByState(string state)
    {
        return await _context.UserCards
                   .Where(card => card.State == state)
                   .ToListAsync()
               ?? throw new EntityNotFoundException($"UserCard with state {state} not found");
    }

    public async Task CreateCard(Entities.UserCard card)
    {
        if (card == null)
        {
            throw new ArgumentNullException($"UserCard entity must not be null");
        }

        var existingUser = await _context.Set<Entities.UserCard>()
            .FirstOrDefaultAsync(c => c.UserName == card.UserName);

        if (existingUser != null)
        {
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