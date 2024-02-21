using Catalog.Data;
using Catalog.Entities;
using Catalog.Exeptions;
using Catalog.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        var entity = await GetProductById(id) ?? throw new EntityNotFoundException($"{typeof(Product).Name} with id {id} not found. Can't delete.");
        //await Task.Run(() => _context.Products.Remove(entity));
        _context.Products.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<Product> GetProductById(Guid id)
    {
        return await _context.Products.FindAsync(id)
               ?? throw new EntityNotFoundException($"{typeof(Product).Name} with id {id} not found.");
    }

    public Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetProductByName(string name)
    {
        //return await _context.Products.ToListAsync(name)
        //?? throw new EntityNotFoundException($"Film with name {name} not found.");
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync()
               ?? throw new Exception($"Couldn't retrieve entities {typeof(Product).Name} ");
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