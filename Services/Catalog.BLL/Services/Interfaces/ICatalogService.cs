using Catalog.BLL.DTOs;
using Catalog.DAL.Entities;

namespace Catalog.BLL.Services.Interfaces;

public interface ICatalogService
{
    Task<ProductDetailsDto> GetProductDetails(string productName);
    Task CreateAllRelations(ProductDetailsDto productDetailsDto);
    Task UpdateProductDetails(ProductDetailsDto productDetailsDto);
    Task CreateProductActorRelation(string productName, string actorName);
    Task DeleteProductActorRelation(string productName, string actorName);
    Task CreateProductGenreRelation(string productName, string genreName);
    Task DeleteProductGenreRelation(string productName, string genreName);
    Task CreateProductDirectorRelation(string productName, string directorName);
    Task DeleteProductDirectorRelation(string productName, string directorName);
    Task CreateProductScreeningRelation(string productName, string scrDate, string scrTime);
    Task DeleteProductScreeningRelation(string productName, string scrDate, string scrTime);
    Task<IEnumerable<Product>> GetProductsByActorName(string actorName);
    Task<IEnumerable<Product>> GetProductsByDirectorName(string directorName);
    Task<IEnumerable<Product>> GetProductsByGenreName(string genreName);
    Task<IEnumerable<MovieScreeningDto>> GetSortedScreeningsAndMoviesByDateTime();
    Task DeleteScreeningByDateTime(string screeningDate, string screeningTime);
    Task ReserveSeat(Guid screeningId, Guid seatId);
    Task<IEnumerable<ScreeningDto>> GetScreeningsWithSeats();
    Task<ScreeningDto?> GetScreeningWithSeatsById(Guid screeningId);
}