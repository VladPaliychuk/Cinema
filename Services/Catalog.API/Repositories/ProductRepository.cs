using Catalog.Data;
using Catalog.Entities;
using Catalog.Exeptions;
using Catalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Repositories;

public class ProductRepository : IProductRepository
{
    protected readonly CatalogContext _context;

    public ProductRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task CreateProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException($"{nameof(Product)} entity must not be null");
        }
        product.Id= Guid.NewGuid();
        _context.Set<Product>().Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProduct(Guid id)
    {
        var entity = await GetProductById(id) 
                     ?? throw new EntityNotFoundException($"Product with id {id} not found. Can't delete.");
        _context.Products.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> GetProductById(Guid id)
    {
        return await _context.Products.FindAsync(id)
               ?? throw new EntityNotFoundException($"Product with id {id} not found.");
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return await _context.Products.ToListAsync();
        }
        var productsByName = await _context.Products
            .Where(product => product.Name
                .ToLower() == name.ToLower())
            .ToListAsync();

        return productsByName
               ?? throw new EntityNotFoundException($"Product with name {name} not found.");
    }
    
    public async Task<Product> GetProductByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        var productByName = await _context.Products
            .FirstOrDefaultAsync(product => product.Name
                .ToLower() == name.ToLower());

        return productByName
               ?? throw new EntityNotFoundException($"Product with name {name} not found.");
    }
    
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync()
               ?? throw new Exception($"Couldn't retrieve entities Product ");
    }

    public async Task UpdateProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException($"{nameof(Product)} entity must not be null");
        }
        
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

}