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

    public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
    {
        if (string.IsNullOrEmpty(categoryName))
        {
            return await _context.Products.ToListAsync();
        }
        var productsByCategory = await _context.Products
            .Where(product => product.Category
                .ToLower() == categoryName.ToLower())
            .ToListAsync();

        return productsByCategory 
               ?? throw new EntityNotFoundException($"Product with category {categoryName} not found.");
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
    
    public async Task<IEnumerable<Product>> GetProductsByNameAndCategory(string name, string category)
    {
        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(category))
        {
            return await _context.Products.ToListAsync();
        }
        var productsByNameAndCategory = await _context.Products
            .Where(product => product.Name
                .ToLower() == name.ToLower() && product.Category.ToLower() == category.ToLower())
            .ToListAsync();

        return productsByNameAndCategory
               ?? throw new EntityNotFoundException($"Product with name {name} and category {category} not found.");
    }
    
    public async Task<IEnumerable<Product>> SearchProducts(string name = null, string category = null)
    {
        var productsByNameAndCategory = await GetProductsByNameAndCategory(name, category);
        var productsByName = await GetProductsByName(name);
        var productsByCategory = await GetProductsByCategory(category);

        return productsByNameAndCategory
            .Concat(productsByName)
            .Concat(productsByCategory)
            .GroupBy(p => p.Id)
            .Select(g => g.First()); // to remove duplicates
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