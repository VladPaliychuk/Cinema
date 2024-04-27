using System.Text.Json.Serialization;

namespace Catalog.Entities;

public class ProductActor
{
    public Guid ProductId { get; set; }
    public Guid ActorId { get; set; }

    [JsonIgnore] public Product Product { get; set; } = null!;
    [JsonIgnore] public Actor Actor { get; set; } = null!;
}