using AutoMapper;
using Catalog.DAL.Entities;
using Catalog.DAL.Entities.DTOs;

namespace Catalog.BLL.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateProductMaps();
        CreateActorMaps();
        CreateGenreMaps();
        CreateScreeningMaps();
    }
    
    private void CreateProductMaps()
    {
        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
    private void CreateActorMaps()
    {
        CreateMap<ActorDto, Actor>();
        CreateMap<Actor, ActorDto>();
    }
    private void CreateGenreMaps()
    {
        CreateMap<GenreDto, Genre>();
        CreateMap<Genre, GenreDto>();
    }
    private void CreateScreeningMaps()
    {
        CreateMap<ScreeningDto, Screening>();
        CreateMap<Screening, ScreeningDto>();
    }
    
}