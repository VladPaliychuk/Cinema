using Catalog.Data;
using Catalog.Entities;
using Catalog.Exeptions;
using Catalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly CatalogContext _context;

    public GenreRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<Genre> GetById(Guid id)
    {
        return await _context.Genres.FindAsync(id)
            ?? throw new EntityNotFoundException($"Genre with Id {id} not found.");
    }
    
    public async Task<Genre> GetByName(string name)
    {
        return await _context.Genres
            .FirstOrDefaultAsync(g => g.Name.ToLower() == name.ToLower())
            ?? throw new EntityNotFoundException($"Genre with name {name} not found.");
    }

    public async Task<IEnumerable<Genre>> GetAll()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task Create(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre != null)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}