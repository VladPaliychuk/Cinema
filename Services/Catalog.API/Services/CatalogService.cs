using Catalog.API.Services.Interfaces.Interfaces;
using Catalog.Entities;
using Catalog.Repositories.Interfaces;

namespace Catalog.API.Services;

public class CatalogService : ICatalogService
{
    //TODO додати емейл розсилку
    private readonly IProductRepository _productRepository;
    private readonly IActorRepository _actorRepository;
    private readonly IProductActorRepository _productActorRepository;

    public CatalogService(IProductRepository productRepository, IActorRepository actorRepository, IProductActorRepository productActorRepository)
    { 
        _productRepository = productRepository;
        _actorRepository = actorRepository;
        _productActorRepository = productActorRepository;
    }

    public async Task CreateProductActorRelation(string productName, string actorName)
    {
        string firstName = actorName.Split(' ')[0];
        string lastName = actorName.Split(' ')[1];
            
        var product = await _productRepository.GetProductByName(productName);
        var actor = await _actorRepository.GetByName(firstName, lastName);

        if (product == null || actor == null)
        { 
            throw new Exception("Product or Actor not found.");
        }

        var productActor = new ProductActor
        {
            ProductId = product.Id,
            ActorId = actor.Id
        };

        await _productActorRepository.Create(productActor);
    }
    
    public async Task<IEnumerable<Product>> GetProductsByActorName(string actorName)
    {
        string firstName = actorName.Split(' ')[0];
        string lastName = actorName.Split(' ')[1];
        
        var actor = await _actorRepository.GetByName(firstName, lastName);

        if (actor == null)
        {
            throw new Exception("Actor not found.");
        }

        var productActors = await _productActorRepository.GetByActorId(actor.Id.ToString());
        var products = productActors.Select(pa => pa.Product).ToList();

        return products;
    }
}