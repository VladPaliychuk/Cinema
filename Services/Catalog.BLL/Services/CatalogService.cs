using System.Globalization;
using AutoMapper;
using Catalog.BLL.DTOs;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Data;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;

namespace Catalog.BLL.Services;

public class CatalogService : ICatalogService
{
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
    
    public async Task<ProductDetailsDto> GetProductDetails(string productName)
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

        var productDetails = new ProductDetailsDto
        {
            Product = _mapper.Map<ProductDto>(product),
            Actors = _mapper.Map<List<ActorDto>>(actors),
            Directors = _mapper.Map<List<DirectorDto>>(directors),
            Genres =  _mapper.Map<List<GenreDto>>(genres),
            Screenings = _mapper.Map<List<ScreeningDto>>(screenings)
        };

        return productDetails;
    }
    
    public async Task CreateAllRelations(ProductDetailsDto productDetailsDto)
    {
        var newProduct = new Product //product.Id= Guid.NewGuid() already in method CreateProduct
        {
            Name = productDetailsDto.Product.Name,
            Summary = productDetailsDto.Product.Summary,
            Description = productDetailsDto.Product.Description,
            ImageFile = productDetailsDto.Product.ImageFile,
            ReleaseDate = productDetailsDto.Product.ReleaseDate,
            Duration = productDetailsDto.Product.Duration,
            Country = productDetailsDto.Product.Country,
            AgeRestriction = productDetailsDto.Product.AgeRestriction,
            Price = productDetailsDto.Product.Price
        };

        await _productRepository.CreateProduct(newProduct);
        
        var product = await _productRepository.GetProductByName(newProduct.Name);
        
        foreach (var actor in productDetailsDto.Actors)
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

        foreach (var director in productDetailsDto.Directors)
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
        
        foreach(var genre in productDetailsDto.Genres)
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
        
        foreach(var screening in productDetailsDto.Screenings)
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

    public async Task UpdateProductDetails(ProductDetailsDto productDetailsDto)
{
    if (productDetailsDto == null)
    {
        throw new ArgumentNullException(nameof(productDetailsDto));
    }
    var product = await _productRepository.GetProductByName(productDetailsDto.Product.Name);
    if (product == null)
    {
        throw new Exception("Product not found.");
    }

    product.Summary = productDetailsDto.Product.Summary;
    product.Description = productDetailsDto.Product.Description;
    product.ImageFile = productDetailsDto.Product.ImageFile;
    product.ReleaseDate = productDetailsDto.Product.ReleaseDate;
    product.Duration = productDetailsDto.Product.Duration;
    product.Country = productDetailsDto.Product.Country;
    product.AgeRestriction = productDetailsDto.Product.AgeRestriction;
    product.Price = productDetailsDto.Product.Price;
    
    await _productRepository.UpdateProduct(product);
    
    // Видалення зв'язків з акторами, які були відсутні в новому запиті
    var existingProductActors = await _productActorRepository.GetByProductId(product.Id);
    
    foreach (var pa in existingProductActors)
    {
        var actor = productDetailsDto.Actors.FirstOrDefault(
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
        var director = productDetailsDto.Directors.FirstOrDefault(a => 
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
        var genre = productDetailsDto.Genres.FirstOrDefault(a => 
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
        var screening = productDetailsDto.Screenings.FirstOrDefault(a => 
            a.StartTime == s.StartTime && 
            a.StartDate == s.StartDate);
        if (screening == null)
        {
            await _screeningRepository.Delete(s.Id);
        }
    }
    
    // Оновлення акторів
    foreach (var actorDto in productDetailsDto.Actors)
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
    foreach (var directorDto in productDetailsDto.Directors)
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
    foreach (var genreDto in productDetailsDto.Genres)
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
    foreach (var screeningDto in productDetailsDto.Screenings)
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

        if (product == null)
        { 
            throw new Exception("Product not found.");
        }

        if (actor == null)
        {
            actor = new Actor
            {
                Id = new Guid(),
                FirstName = firstName,
                LastName = lastName
            };
            await _actorRepository.Create(actor);
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

    public async Task CreateProductGenreRelation(string productName, string genreName)
    {
        var product = await _productRepository.GetProductByName(productName);
        var genre = await _genreRepository.GetByName(genreName);

        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        if(genre == null)
        {
            genre = new Genre
            {
                Id = Guid.NewGuid(),
                Name = genreName
            };
            await _genreRepository.Create(genre);
        }

        var productGenre = new ProductGenre
        {
            ProductId = product.Id,
            GenreId = genre.Id
        };

        await _productGenreRepository.Create(productGenre);
    }

    public async Task DeleteProductGenreRelation(string productName, string genreName)
    {
        var product = await _productRepository.GetProductByName(productName);
        var genre = await _genreRepository.GetByName(genreName);

        if (product == null || genre == null)
        {
            throw new Exception("Product or Genre not found.");
        }

        await _productGenreRepository.Delete(product.Id, genre.Id);
    }

    public async Task CreateProductScreeningRelation(string productName, string scrDate, string scrTime)
    {
        var product = await _productRepository.GetProductByName(productName);
        var screening = await _screeningRepository.GetByDateTime(scrDate, scrTime);

        if (screening == null)
        {
            screening = new Screening
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                StartTime = scrTime,
                StartDate = scrDate
            };
            await _screeningRepository.Create(screening);
        }
        
        for (int i = 1; i <= 6; i++)
        {
            for (int j = 1; j <= 6; j++)
            {
                var seat = new Seat
                {
                    Id = Guid.NewGuid(),
                    Number = j.ToString(),
                    Row = i.ToString(),
                    ScreeningId = screening.Id,
                    IsReserved = false
                };
                await _seatRepository.Create(seat);
            }
        }
    }
    
    public async Task DeleteProductScreeningRelation(string productName, string scrDate, string scrTime)
    {
        var product = await _productRepository.GetProductByName(productName);
        var screening = await _screeningRepository.GetByDateTime(scrDate, scrTime);

        if (product == null || screening == null)
        {
            throw new Exception("Product or Screening not found.");
        }

        await _screeningRepository.Delete(screening.Id);
    }

    public async Task CreateProductDirectorRelation(string productName, string directorName)
    {
        string firstName = directorName.Split(' ')[0];
        string lastName = directorName.Split(' ')[1];

        var product = await _productRepository.GetProductByName(productName);
        var director = await _directorRepository.GetByName(firstName, lastName);

        if (product == null)
        {
            throw new Exception("Product");
        }

        if (director == null)
        {
            director = new Director
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName
            };
            await _directorRepository.Create(director);
        }

        var productDirector = new ProductDirector
        {
            ProductId = product.Id,
            DirectorId = director.Id
        };

        await _productDirectorRepository.Create(productDirector);
    }

    public async Task DeleteProductDirectorRelation(string productName, string directorName)
    {
        string firstName = directorName.Split(' ')[0];
        string lastName = directorName.Split(' ')[1];

        var product = await _productRepository.GetProductByName(productName);
        var director = await _directorRepository.GetByName(firstName, lastName);

        if (product == null || director == null)
        {
            throw new Exception("Product or Director not found.");
        }

        await _productDirectorRepository.Delete(product.Id, director.Id);
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
    
    public async Task<IEnumerable<MovieScreeningDto>> GetSortedScreeningsAndMoviesByDateTime()
    {
        var screenings = await _screeningRepository.GetAllWithProductAsync();

        // Convert the screenings to a list and parse the dates and times for sorting
        var sortedScreenings = screenings
            .OrderByDescending(s => DateTime.ParseExact(s.StartDate, "d MMMM", CultureInfo.GetCultureInfo("uk-UA")))
            .ThenByDescending(s => DateTime.ParseExact(s.StartTime, "HH:mm", CultureInfo.InvariantCulture))
            .ToList();

        // Map sorted screenings to DTOs
        var mappedScreenings = sortedScreenings.Select(s => new MovieScreeningDto
        {
            Product = s.Product,
            Screening = s
        });

        return mappedScreenings;
    }
    
    public async Task DeleteScreeningByDateTime(string screeningDate, string screeningTime)
    {
        var screening = await _screeningRepository.GetByDateTime(screeningDate, screeningTime);
        if (screening == null)
        {
            throw new Exception("Screening not found.");
        }

        await _screeningRepository.Delete(screening.Id);
    }
    
    public async Task<IEnumerable<ScreeningDto>> GetScreeningsWithSeats()
    {
        var screenings = await _screeningRepository.GetAllScreeningsWithSeatsAsync();

        var mappedScreenings = _mapper.Map<IEnumerable<ScreeningDto>>(screenings);

        return mappedScreenings;
    }
    
    public async Task<ScreeningDto?> GetScreeningWithSeatsById(Guid screeningId)
    {
        var screening = await _screeningRepository.GetScreeningWithSeatsByIdAsync(screeningId);
    
        if (screening == null)
        {
            return null;
        }
    
        var mappedScreening = _mapper.Map<ScreeningDto>(screening);
        return mappedScreening;
    }
    
    public async Task ReserveSeat(Guid screeningId, Guid seatId)
    {
        var seat = await _seatRepository.GetById(seatId);

        if (seat.IsReserved)
        {
            throw new Exception("Seat is already reserved.");
        }

        if (seat.ScreeningId != screeningId)
        {
            throw new Exception("Seat does not belong to the given screening.");
        }

        seat.IsReserved = true;

        await _seatRepository.Update(seat);
    }
}