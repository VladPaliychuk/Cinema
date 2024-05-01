namespace Catalog.BLL.DTOs;

public class ProductDto
{
    public string Name { get; set; } = null!;
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? ImageFile { get; set; }
    public string? ReleaseDate { get; set; }
    public string Duration { get; set; } = null!;
    public string Country { get; set; }  = null!;
    public string AgeRestriction { get; set; } = null!;
    public decimal Price { get; set; } 
}