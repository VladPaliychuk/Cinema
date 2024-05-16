using Catalog.DAL.Entities;

namespace Catalog.BLL.DTOs;

public class MovieScreeningDto
{
    public Product Product { get; set; } = null!;
    public Screening Screening { get; set; } = null!;
}