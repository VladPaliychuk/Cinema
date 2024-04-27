namespace Catalog.Entities.DTOs;

public class ProductDetails
{
    public Product Product { get; set; }
    public IEnumerable<Genre> Genres { get; set; }
    public IEnumerable<Actor> Actors { get; set; }
    public IEnumerable<Screening> Screenings { get; set; }
    public IEnumerable<Seat> Seats { get; set; }
}