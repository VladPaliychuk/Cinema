using System.Text.Json.Serialization;

namespace Catalog.DAL.Entities;

public class ProductDirector
{
    public Guid ProductId { get; set; }
    public Guid DirectorId { get; set; }

    [JsonIgnore] public Product Product { get; set; } = null!;
    [JsonIgnore] public Director Director { get; set; } = null!;
}