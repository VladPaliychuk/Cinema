using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Exeptions;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DAL.Repositories;

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

    public async Task<IEnumerable<ProductActor>> GetByProductId(Guid productId)
    {
        
        return await _context.ProductActors
            .Where(productActor => productActor.ProductId== productId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<ProductActor>> GetByActorId(Guid actorId)
    {
        return await _context.ProductActors
            .Where(productActor => productActor.ActorId == actorId)
            .ToListAsync();
    }

    public async Task<ProductActor> GetByProductIdAndActorId(Guid productId, Guid actorId)
    {
        return await _context.ProductActors
            .FirstOrDefaultAsync(productActor => productActor.ProductId == productId && productActor.ActorId == actorId);
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

    public async Task Delete(Guid productId, Guid actorId)
    {
        var productActor = await _context.ProductActors
            .FirstOrDefaultAsync(pa => pa.ProductId == productId && pa.ActorId == actorId);

        if (productActor == null)
        {
            throw new EntityNotFoundException($"ProductActor with ProductId {productId} and ActorId {actorId} not found.");
        }

        _context.ProductActors.Remove(productActor);
        await _context.SaveChangesAsync();
    }
}