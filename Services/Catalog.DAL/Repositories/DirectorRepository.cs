using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Exeptions;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.Repositories;

public class DirectorRepository : IDirectorRepository
{
    private readonly CatalogContext _context;

    public DirectorRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<Director> GetById(Guid id)
    {
        return await _context.Directors.FindAsync(id);
    }

    public async Task<IEnumerable<Director>> GetAll()
    {
        return await _context.Directors.ToListAsync();
    }

    public async Task<Director> GetByName(string firstName, string lastName)
    {
        return await _context.Directors.FirstOrDefaultAsync(director =>
            director.FirstName == firstName && director.LastName == lastName);
    }

    public async Task Create(Director director)
    {
        if (director == null)
        {
            throw new ArgumentNullException(nameof(director));
        }
        
        await _context.Directors.AddAsync(director);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Director director)
    {
        if (director == null)
        {
            throw new ArgumentNullException(nameof(director));
        }

        _context.Directors.Update(director);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var director = await _context.Directors.FindAsync(id);
        if (director == null)
        {
            throw new EntityNotFoundException($"Actor with id {id} not found.");
        }

        _context.Directors.Remove(director);
        await _context.SaveChangesAsync();
    }
}