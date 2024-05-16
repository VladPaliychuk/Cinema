namespace Catalog.BLL.DTOs;

public class ScreeningDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string StartTime { get; set; } = null!;
    public string StartDate { get; set; } = null!;
    public ICollection<SeatDto>? Seats { get; set; }
}