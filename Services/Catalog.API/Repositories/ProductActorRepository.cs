using Catalog.Data;
using Catalog.Entities;
using Catalog.Exeptions;
using Catalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories;

public class ProductActorRepository : IProductActorRepository
{
    private readonly CatalogContext _context;

    public ProductActorRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductActor>> GetAll()
    {
        return await _context.ProductActors.ToListAsync();
    }

    public async Task<IEnumerable<ProductActor>> GetByProductId(string productId)
    {
        if (string.IsNullOrEmpty(productId))
        {
            throw new ArgumentNullException(nameof(productId));
        }
        
        return await _context.ProductActors
            .Where(productActor => productActor.ProductId.ToString() == productId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ProductActor>> GetByActorId(string actorId)
    {
        if (string.IsNullOrEmpty(actorId))
        {
            throw new ArgumentNullException(nameof(actorId));
        }
        
        return await _context.ProductActors
            .Where(productActor => productActor.ActorId.ToString() == actorId)
            .ToListAsync();
    }
    
    public async Task Create(ProductActor productActor)
    {
        if (productActor == null)
        {
            throw new ArgumentNullException(nameof(productActor));
        }

        await _context.ProductActors.AddAsync(productActor);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ProductActor productActor)
    {
        if (productActor == null)
        {
            throw new ArgumentNullException(nameof(productActor));
        }

        _context.ProductActors.Update(productActor);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var productActor = await _context.ProductActors.FindAsync(id);
        if (productActor == null)
        {
            throw new EntityNotFoundException($"ProductActor with id {id} not found.");
        }

        _context.ProductActors.Remove(productActor);
        await _context.SaveChangesAsync();
    }
}