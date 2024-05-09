using Catalog.BLL.DTOs;
using Catalog.DAL.Entities;

namespace Catalog.BLL.Services.Interfaces;

public interface ICatalogService
{
    Task<ProductDetails> GetProductDetails(string productName);
    Task CreateAllRelations(ProductDetails productDetails);
    Task UpdateProductDetails(ProductDetails productDetails);
    Task CreateProductActorRelation(string productName, string actorName);
    Task DeleteProductActorRelation(string productName, string actorName);
    Task CreateProductGenreRelation(string productName, string genreName);
    Task DeleteProductGenreRelation(string productName, string genreName);
    Task CreateProductDirectorRelation(string productName, string directorName);
    Task DeleteProductDirectorRelation(string productName, string directorName);
    Task<IEnumerable<Product>> GetProductsByActorName(string actorName);
    Task<IEnumerable<Product>> GetProductsByDirectorName(string directorName);
    Task<IEnumerable<Product>> GetProductsByGenreName(string genreName);
}