using System.Text.Json.Serialization;

namespace Catalog.DAL.Entities;

public class Director
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore] public ICollection<ProductDirector> ProductDirectors { get; set; } = null!;
}