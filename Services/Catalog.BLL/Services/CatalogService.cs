using AutoMapper;
using Catalog.BLL.DTOs;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;

namespace Catalog.BLL.Services;

public class CatalogService : ICatalogService
{
    //TODO додати емейл розсилку
    
    private readonly IMapper _mapper;
    private readonly CatalogContext _context;
    private readonly IProductRepository _productRepository;
    private readonly IActorRepository _actorRepository;
    private readonly IProductActorRepository _productActorRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IProductGenreRepository _productGenreRepository;
    private readonly IScreeningRepository _screeningRepository;
    private readonly ISeatRepository _seatRepository;
    private readonly IDirectorRepository _directorRepository;
    private readonly IProductDirectorRepository _productDirectorRepository;
    

    public CatalogService(
        IMapper mapper,
        CatalogContext context,
        IProductRepository productRepository,
        IActorRepository actorRepository,
        IProductActorRepository productActorRepository,
        IGenreRepository genreRepository,
        IProductGenreRepository productGenreRepository,
        IScreeningRepository screeningRepository,
        ISeatRepository seatRepository,
        IDirectorRepository directorRepository,
        IProductDirectorRepository productDirectorRepository)
    {
        _mapper = mapper;
        _context = context;
        _productRepository = productRepository;
        _actorRepository = actorRepository;
        _productActorRepository = productActorRepository;
        _genreRepository = genreRepository;
        _productGenreRepository = productGenreRepository;
        _screeningRepository = screeningRepository;
        _seatRepository = seatRepository;
        _directorRepository = directorRepository;
        _productDirectorRepository = productDirectorRepository;
    }
    
    public async Task<ProductDetails> GetProductDetails(string productName)
    {
        var product = await _productRepository.GetProductByName(productName);

        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        var productActors = await _productActorRepository.GetByProductId(product.Id);
        List<Actor> actors = new List<Actor>();
        foreach(var pa in productActors)
        {
            var actor = await _actorRepository.GetById(pa.ActorId);
            actors.Add(actor);  
        }
        
        var productDirectors = await _productDirectorRepository.GetByProductId(product.Id.ToString());
        List<Director> directors = new List<Director>();
        foreach (var pa in productDirectors)
        {
            var director = await _directorRepository.GetById(pa.DirectorId);
            directors.Add(director);
        }

        var productGenres = await _productGenreRepository.GetByProductId(product.Id.ToString());
        List<Genre> genres = new List<Genre>();
        foreach(var genre in productGenres)
        {
            var genreEntity = await _genreRepository.GetById(genre.GenreId);
            genres.Add(genreEntity);
        }
        
        var screenings = await _screeningRepository.GetByProductId(product.Id);

        var productDetails = new ProductDetails
        {
            Product = _mapper.Map<ProductDto>(product),
            Actors = _mapper.Map<List<ActorDto>>(actors),
            Directors = _mapper.Map<List<DirectorDto>>(directors),
            Genres =  _mapper.Map<List<GenreDto>>(genres),
            Screenings = _mapper.Map<List<ScreeningDto>>(screenings)
        };

        return productDetails;
    }
    public async Task CreateAllRelations(ProductDetails productDetails)
    {
        var newProduct = new Product //product.Id= Guid.NewGuid() already in method CreateProduct
        {
            Name = productDetails.Product.Name,
            Summary = productDetails.Product.Summary,
            Description = productDetails.Product.Description,
            ImageFile = productDetails.Product.ImageFile,
            ReleaseDate = productDetails.Product.ReleaseDate,
            Duration = productDetails.Product.Duration,
            Country = productDetails.Product.Country,
            AgeRestriction = productDetails.Product.AgeRestriction,
            Price = productDetails.Product.Price
        };

        await _productRepository.CreateProduct(newProduct);
        
        var product = await _productRepository.GetProductByName(newProduct.Name);
        
        foreach (var actor in productDetails.Actors)
        {
            var actorEntity = await _actorRepository.GetByName(actor.FirstName, actor.LastName);
            if (actorEntity == null)
            {
                actorEntity = new Actor
                {
                    Id = new Guid(),
                    FirstName = actor.FirstName,
                    LastName = actor.LastName
                };
                await _actorRepository.Create(actorEntity);
            }

            var productActor = new ProductActor
            {
                ProductId = product.Id,
                ActorId = actorEntity.Id
            };

            await _productActorRepository.Create(productActor);
        }

        foreach (var director in productDetails.Directors)
        {
            var directorEntity = await _directorRepository.GetByName(director.FirstName, director.LastName);
            if (directorEntity == null)
            {
                directorEntity = new Director
                {
                    Id = new Guid(),
                    FirstName = director.FirstName,
                    LastName = director.LastName
                };
                await _directorRepository.Create(directorEntity);
            }
            
            var directorProduct = new ProductDirector
            {
                ProductId = product.Id,
                DirectorId = directorEntity.Id
            };
            
            await _productDirectorRepository.Create(directorProduct);
        }
        
        foreach(var genre in productDetails.Genres)
        {
            var genreEntity = await _genreRepository.GetByName(genre.Name);
            
            if (genreEntity == null)
            {
                genreEntity = new Genre
                {
                    Id = new Guid(),
                    Name = genre.Name
                };
                await _genreRepository.Create(genreEntity);
            }

            var productGenre = new ProductGenre
            {
                ProductId = product.Id,
                GenreId = genreEntity.Id
            };

            await _productGenreRepository.Create(productGenre);
        }
        
        foreach(var screening in productDetails.Screenings)
        {
            var newScreening = new Screening
            {
                Id = new Guid(),
                ProductId = product.Id,
                StartTime = screening.StartTime,
                StartDate = screening.StartDate
            };
            await _screeningRepository.Create(newScreening);

            for (int i = 1; i <= 6; i++)
            {
                for (int j = 1; j <= 6; j++)
                {
                    var newSeat = new Seat
                    {
                        Id = new Guid(),
                        Number = j.ToString(),
                        Row = i.ToString(),
                        ScreeningId = newScreening.Id,
                        IsReserved = false
                    };
                    await _seatRepository.Create(newSeat);
                }
            }
        }
    }

    public async Task UpdateProductDetails(ProductDetails productDetails)
{
    if (productDetails == null)
    {
        throw new ArgumentNullException(nameof(productDetails));
    }
    var product = await _productRepository.GetProductByName(productDetails.Product.Name);
    if (product == null)
    {
        throw new Exception("Product not found.");
    }

    product.Summary = productDetails.Product.Summary;
    product.Description = productDetails.Product.Description;
    product.ImageFile = productDetails.Product.ImageFile;
    product.ReleaseDate = productDetails.Product.ReleaseDate;
    product.Duration = productDetails.Product.Duration;
    product.Country = productDetails.Product.Country;
    product.AgeRestriction = productDetails.Product.AgeRestriction;
    product.Price = productDetails.Product.Price;
    
    await _productRepository.UpdateProduct(product);
    
    // Видалення зв'язків з акторами, які були відсутні в новому запиті
    var existingProductActors = await _productActorRepository.GetByProductId(product.Id);
    
    foreach (var pa in existingProductActors)
    {
        var actor = productDetails.Actors.FirstOrDefault(
            a => 
            a.FirstName == pa.Actor.FirstName && 
            a.LastName == pa.Actor.LastName);
        if (actor == null)
        {
            await _productActorRepository.Delete(pa.ProductId, pa.ActorId);
        }
    }
    
    // Видалення зв'язків з режисерами, які були відсутні в новому запиті
    var existingProductDirectors = await _productDirectorRepository.GetByProductId(product.Id.ToString());
    foreach (var pd in existingProductDirectors)
    {
        var director = productDetails.Directors.FirstOrDefault(a => 
            a.FirstName == pd.Director.FirstName && 
            a.LastName == pd.Director.LastName);
        if (director == null)
        {
            await _productDirectorRepository.Delete(pd.ProductId, pd.DirectorId);
        }
    }
    
    // Видалення зв'язків з жанрами, які були відсутні в новому запиті
    var existingProductGenres = await _productGenreRepository.GetByProductId(product.Id.ToString());
    foreach (var pg in existingProductGenres)
    {
        var genre = productDetails.Genres.FirstOrDefault(a => 
            a.Name == pg.Genre.Name);
        if (genre == null)
        {
            await _productGenreRepository.Delete(pg.ProductId, pg.GenreId);
        }
    }
    
    // Видалення зв'язків з screening, які були відсутні в новому запиті
    var existingScreenings = await _screeningRepository.GetByProductId(product.Id);
    foreach (var s in existingScreenings)
    {
        var screening = productDetails.Screenings.FirstOrDefault(a => 
            a.StartTime == s.StartTime && 
            a.StartDate == s.StartDate);
        if (screening == null)
        {
            await _screeningRepository.Delete(s.Id);
        }
    }
    
    // Оновлення акторів
    foreach (var actorDto in productDetails.Actors)
    {
        var actor = await _actorRepository.GetByName(actorDto.FirstName, actorDto.LastName);
        if (actor == null)
        {
            actor = new Actor
            {
                Id = Guid.NewGuid(),
                FirstName = actorDto.FirstName,
                LastName = actorDto.LastName
            };
            await _actorRepository.Create(actor);
        }

        var productActor = await _productActorRepository.GetByProductIdAndActorId(product.Id, actor.Id);
        if (productActor == null)
        {
            productActor = new ProductActor
            {
                ProductId = product.Id,
                ActorId = actor.Id
            };
            await _productActorRepository.Create(productActor);
        }
    }
    
    // Оновлення режисерів
    foreach (var directorDto in productDetails.Directors)
    {
        var director = await _directorRepository.GetByName(directorDto.FirstName, directorDto.LastName);
        if(director == null)
        {
            director = new Director
            {
                Id = Guid.NewGuid(),
                FirstName = directorDto.FirstName,
                LastName = directorDto.LastName
            };
            await _directorRepository.Create(director);
        }

        var productDirector = await _productDirectorRepository.GetByProductIdAndDirectorId(product.Id, director.Id);
        if (productDirector == null)
        {
            productDirector = new ProductDirector
            {
                ProductId = product.Id,
                DirectorId = director.Id
            };
            await _productDirectorRepository.Create(productDirector);
        }
    }
    
    // Оновлення жанрів
    foreach (var genreDto in productDetails.Genres)
    {
        var genre = await _genreRepository.GetByName(genreDto.Name);
        if (genre == null)
        {
            genre = new Genre
            {
                Id = Guid.NewGuid(),
                Name = genreDto.Name
            };
            await _genreRepository.Create(genre);
        }

        var productGenre = await _productGenreRepository.GetByProductIdAndGenreId(product.Id, genre.Id);
        if (productGenre == null)
        {
            productGenre = new ProductGenre
            {
                ProductId = product.Id,
                GenreId = genre.Id
            };
            await _productGenreRepository.Create(productGenre);
        }
    }
    
    // Оновлення сеансів
    foreach (var screeningDto in productDetails.Screenings)
    {
        var screening = await _screeningRepository.GetByDateTime(screeningDto.StartDate, screeningDto.StartTime);
        if (screening == null)
        {
            screening = new Screening
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                StartTime = screeningDto.StartTime,
                StartDate = screeningDto.StartDate
            };
            await _screeningRepository.Create(screening);
        }

        var existingScreening = await _screeningRepository.GetByDateTime(screeningDto.StartDate, screeningDto.StartTime);
        if (existingScreening == null)
        {
            await _screeningRepository.Create(screening);
        }
    }

    await _context.SaveChangesAsync();
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
    
    public async Task DeleteProductActorRelation(string productName, string actorName)
    {
        string firstName = actorName.Split(' ')[0];
        string lastName = actorName.Split(' ')[1];
        
        var product = await _productRepository.GetProductByName(productName);
        var actor = await _actorRepository.GetByName(firstName, lastName);
        
        if (product == null || actor == null)
        {
            throw new Exception("Product or Actor not found.");
        }
        
        await _productActorRepository.Delete(product.Id, actor.Id);
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

        var productActors = await _productActorRepository.GetByActorId(actor.Id);
        List<Product> products = new List<Product>();
        foreach(var pa in productActors)
        {
            var product = await _productRepository.GetProductById(pa.ProductId);
            products.Add(product);  
        }
        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByDirectorName(string directorName)
    {
        string firstName = directorName.Split(' ')[0];
        string lastName = directorName.Split(' ')[1];
        
        var director = await _directorRepository.GetByName(firstName, lastName);
        
        if(director == null){throw new Exception("Actor not found.");}
        
        var productDirectors = await _productDirectorRepository.GetByDirectorId(director.Id);
        List<Product> products = new List<Product>();
        foreach(var pd in productDirectors)
        {
            var product = await _productRepository.GetProductById(pd.ProductId);
            products.Add(product);
        }

        return products;
    }

    public async Task<IEnumerable<Product>> GetProductsByGenreName(string genreName)
    {
        var genre = await _genreRepository.GetByName(genreName);
        
        if(genre == null){throw new Exception("Genre not found.");}
        
        var productGenres = await _productGenreRepository.GetByGenreId(genre.Id);
        List<Product> products = new List<Product>();
        foreach(var pg in productGenres)
        {
            var product = await _productRepository.GetProductById(pg.ProductId);
            products.Add(product);
        }

        return products;
    }
    
}