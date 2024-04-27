namespace Catalog.DAL.Entities.DTOs;

public class ProductDto
{
    public string Name { get; set; } = null!;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? ImageFile { get; set; }
    public string? ReleaseDate { get; set; }
    public decimal Price { get; set; } 
}