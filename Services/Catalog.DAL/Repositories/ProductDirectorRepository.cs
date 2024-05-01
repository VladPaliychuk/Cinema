using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Exeptions;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.Repositories;

public class ProductDirectorRepository : IProductDirectorRepository
{
    private readonly CatalogContext _context;
    
    public ProductDirectorRepository(CatalogContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProductDirector>> GetAll()
    {
        return await _context.ProductDirectors.ToListAsync();
    }

    public async Task<IEnumerable<ProductDirector>> GetByProductId(string productId)
    {
        if (string.IsNullOrEmpty(productId))
        {
            throw new ArgumentNullException(nameof(productId));
        }
        
        return await _context.ProductDirectors
            .Where(productDirector => productDirector.ProductId.ToString() == productId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductDirector>> GetByDirectorId(Guid directorId)
    {
        if (string.IsNullOrEmpty(directorId.ToString()))
        {
            throw new ArgumentNullException(nameof(directorId));
        }
        
        return await _context.ProductDirectors
            .Where(productDirector => productDirector.DirectorId == directorId)
            .ToListAsync();
    }

    public async Task Create(ProductDirector productDirector)
    {
        if (productDirector == null)
        {
            throw new ArgumentNullException(nameof(productDirector));
        }

        await _context.ProductDirectors.AddAsync(productDirector);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ProductDirector productDirector)
    {
        if (productDirector == null)
        {
            throw new ArgumentNullException(nameof(productDirector));
        }

        _context.ProductDirectors.Update(productDirector);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var productDirector = await _context.ProductDirectors.FindAsync(id);
        if (productDirector == null)
        {
            throw new EntityNotFoundException($"ProductDirector with id {id} not found.");
        }

        _context.ProductDirectors.Remove(productDirector);
        await _context.SaveChangesAsync();
    }
}