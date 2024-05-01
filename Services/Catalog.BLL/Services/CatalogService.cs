﻿using AutoMapper;
using Catalog.BLL.DTOs;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;

namespace Catalog.BLL.Services;

public class CatalogService : ICatalogService
{
    //TODO додати емейл розсилку
    
    private readonly IMapper _mapper;
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
    
    public async Task<ProductDetails> GetProductDetails(string productName)
    {
        var product = await _productRepository.GetProductByName(productName);

        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        var productActors = await _productActorRepository.GetByProductId(product.Id.ToString());
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