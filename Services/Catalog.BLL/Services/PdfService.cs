using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.Repositories.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Catalog.BLL.Services;

using System;
using System.IO;
using System.Threading.Tasks;

public class PdfService : IPdfService
{
    private readonly ISeatRepository _seatRepository;
    private readonly IScreeningRepository _screeningRepository;

    public PdfService(ISeatRepository seatRepository, IScreeningRepository screeningRepository)
    {
        _seatRepository = seatRepository;
        _screeningRepository = screeningRepository;
    }

    public async Task<byte[]> GenerateReservationPdfAsync(Guid seatId, string username)
    {
        var seat = await _seatRepository.GetSeatWithScreeningAsync(seatId);
        var screening = seat.Screening;
        var product = screening.Product;

        using (var ms = new MemoryStream())
        {
            var document = new Document();
            var writer = PdfWriter.GetInstance(document, ms);
            document.Open();

            document.Add(new Paragraph("Reservation Details"));
            document.Add(new Paragraph($"Movie: {product.Name}"));
            document.Add(new Paragraph($"Description: {product.Description}"));
            document.Add(new Paragraph($"Screening Date: {screening.StartDate}"));
            document.Add(new Paragraph($"Screening Time: {screening.StartTime}"));
            document.Add(new Paragraph($"Seat: {seat.Row} - {seat.Number}"));
            document.Add(new Paragraph($"User: {username}"));

            document.Close();
            writer.Close();

            return ms.ToArray();
        }
    }
}
