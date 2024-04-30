using System.Text.Json.Serialization;

namespace Catalog.DAL.Entities;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    [JsonIgnore] public ICollection<ProductGenre> ProductsGenres { get; set; } = null!;
}