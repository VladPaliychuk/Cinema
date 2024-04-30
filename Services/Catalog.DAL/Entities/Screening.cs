using System.Text.Json.Serialization;

namespace Catalog.DAL.Entities;

public class Screening
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string StartTime { get; set; }
    public string StartDate { get; set; }
    
    [JsonIgnore] public Product Product { get; set; } = null!;
    
    [JsonIgnore] public ICollection<Seat>? Seats { get; set; }
}