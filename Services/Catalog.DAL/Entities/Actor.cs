using System.Text.Json.Serialization;

namespace Catalog.DAL.Entities;

public class Actor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore] public ICollection<ProductActor> ProductActors { get; set; } = null!;
}