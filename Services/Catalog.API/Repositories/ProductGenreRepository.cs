using Catalog.Data;
using Catalog.Entities;
using Catalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories;

public class ProductGenreRepository : IProductGenreRepository
{
    private readonly CatalogContext _context;

    public ProductGenreRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductGenre>> GetAll()
    {
        return await _context.ProductGenres.ToListAsync();
    }

    public async Task<IEnumerable<ProductGenre>> GetByProductId(string productId)
    {
        return await _context.ProductGenres
            .Where(pg => pg.ProductId.ToString() == productId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductGenre>> GetByGenreId(Guid genreId)
    {
        return await _context.ProductGenres
            .Where(pg => pg.GenreId == genreId)
            .ToListAsync();
    }

    public async Task Create(ProductGenre productGenre)
    {
        _context.ProductGenres.Add(productGenre);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ProductGenre productGenre)
    {
        _context.ProductGenres.Update(productGenre);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var productGenre = await _context.ProductGenres.FindAsync(id);
        if (productGenre != null)
        {
            _context.ProductGenres.Remove(productGenre);
            await _context.SaveChangesAsync();
        }
    }
}