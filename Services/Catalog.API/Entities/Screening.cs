using System.Text.Json.Serialization;

namespace Catalog.Entities;

public class Screening
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string StartScreening { get; set; }
    
    [JsonIgnore] public Product Product { get; set; } = null!;
    
    [JsonIgnore] public ICollection<Seat>? Seats { get; set; }
}