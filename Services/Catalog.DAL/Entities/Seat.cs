namespace Catalog.DAL.Entities;

public class Seat
{
    public Guid Id { get; set; }
    public string Row { get; set; }
    public string Number { get; set; }
    public bool IsReserved { get; set; }
    
    public Guid ScreeningId { get; set; }
    public Screening Screening { get; set; } = null!;
}