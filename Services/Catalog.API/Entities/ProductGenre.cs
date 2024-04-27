using System.Text.Json.Serialization;

namespace Catalog.Entities;

public class ProductGenre
{
    public Guid ProductId { get; set; }
    public Guid GenreId { get; set; }

    [JsonIgnore] public Product Product { get; set; } = null!;
    [JsonIgnore] public Genre Genre { get; set; } = null!;
}