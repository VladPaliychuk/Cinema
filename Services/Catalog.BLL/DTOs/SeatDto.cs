namespace Catalog.DAL.Entities.DTOs;

public class SeatDto
{
    public string Row { get; set; } = null!;
    public string Number { get; set; } = null!;
    public bool IsReserved { get; set; }
}