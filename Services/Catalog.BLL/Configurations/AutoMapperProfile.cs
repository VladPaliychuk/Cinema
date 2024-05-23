using AutoMapper;
using Catalog.BLL.DTOs;
using Catalog.DAL.Entities;

namespace Catalog.BLL.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateProductMaps();
        CreateActorMaps();
        CreateGenreMaps();
        CreateScreeningMaps();
        CreateSeatMaps();
        CreateDirectorMaps();
        CreateMovieScreeningMaps();
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
        CreateMap<Screening, ScreeningDto>()
            .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats));
        CreateMap<Screening, ScreeningDto>();
        
    }
    private void CreateSeatMaps()
    {
        CreateMap<SeatDto, Seat>();
        CreateMap<Seat, SeatDto>();
    }
    private void CreateDirectorMaps()
    {
        CreateMap<DirectorDto, Director>();
        CreateMap<Director, DirectorDto>();
    }
    
    private void CreateMovieScreeningMaps()
    {
        CreateMap<Product, MovieScreeningDto>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src));
        
        CreateMap<Screening, MovieScreeningDto>()
            .ForMember(dest => dest.Screening, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
    }
}