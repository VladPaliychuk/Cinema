using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Exeptions;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.Repositories;

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
    
    public async Task<ProductGenre> GetByProductIdAndGenreId(Guid productId, Guid genreId)
    {
        return await _context.ProductGenres
            .FirstOrDefaultAsync(pg => pg.ProductId == productId && pg.GenreId == genreId);
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

    public async Task Delete(Guid productId, Guid genreId)
    {
        var productGenre = await _context.ProductGenres
            .FirstOrDefaultAsync(pa => pa.ProductId == productId && pa.GenreId == genreId);

        if (productGenre == null)
        {
            throw new EntityNotFoundException($"ProductGenre with ProductId {productId} " +
                                              $"and GenreId {genreId} not found.");
        }

        _context.ProductGenres.Remove(productGenre);
        await _context.SaveChangesAsync();
    }
}