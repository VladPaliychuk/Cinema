using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Exeptions;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly CatalogContext _context;

    public ActorRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Actor>> GetAll()
    {
        return await _context.Actors.ToListAsync();
    }

    public async Task<Actor> GetById(Guid id)
    {
        return await _context.Actors.FindAsync(id);
    }
    
    public async Task<Actor> GetByName(string firstName, string lastName)
    {
        return await _context.Actors.FirstOrDefaultAsync(actor =>
            actor.FirstName == firstName && actor.LastName == lastName);
    }

    public async Task Create(Actor actor)
    {
        if (actor == null)
        {
            throw new ArgumentNullException(nameof(actor));
        }
        
        await _context.Actors.AddAsync(actor);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Actor actor)
    {
        if (actor == null)
        {
            throw new ArgumentNullException(nameof(actor));
        }

        _context.Actors.Update(actor);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor == null)
        {
            throw new EntityNotFoundException($"Actor with id {id} not found.");
        }

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();
    }
}