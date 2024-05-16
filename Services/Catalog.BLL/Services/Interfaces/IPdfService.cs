namespace Catalog.BLL.Services.Interfaces;

public interface IPdfService
{
    Task<byte[]> GenerateReservationPdfAsync(Guid seatId, string username);
}
