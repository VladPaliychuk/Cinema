namespace Catalog.BLL.DTOs;

public class SeatDto
{
    public Guid Id { get; set; }
    public string Row { get; set; } = null!;
    public string Number { get; set; } = null!;
    public bool IsReserved { get; set; }
}