using System.Text;
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

    /*public async Task<byte[]> GenerateReservationPdfAsync(Guid seatId, string username)
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
            document.Add(new Paragraph($"Screening Date: {screening.StartDate:yyyy-MM-dd}"));
            document.Add(new Paragraph($"Screening Time: {screening.StartTime:HH:mm}"));
            document.Add(new Paragraph($"Seat: {seat.Row} - {seat.Number}"));
            document.Add(new Paragraph($"User: {username}"));
            document.Add(new Paragraph($"Base directory: {AppDomain.CurrentDomain.BaseDirectory}"));
            document.Close();
            writer.Close();

            return ms.ToArray();
        }
    }*/
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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // Завантаження шрифту з папки Resources
            var fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "timesnewromanpsmt.ttf");
            if (!File.Exists(fontPath))
            {
                Console.WriteLine($"Font file not found at: {fontPath}");
                throw new FileNotFoundException("Font file not found", fontPath);
            }

            var baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            var font = new Font(baseFont, 12, Font.NORMAL);

            // Додавання тексту з використанням обраного шрифту
            document.Add(new Paragraph("Деталі бронювання", font));
            document.Add(new Paragraph($"Фільм: {product.Name}", font));
            document.Add(new Paragraph($"Опис: {product.Description}", font));
            document.Add(new Paragraph($"Дата показу: {screening.StartDate}", font));
            document.Add(new Paragraph($"Час показу: {screening.StartTime}", font));
            document.Add(new Paragraph($"Місце: {seat.Row} - {seat.Number}", font));
            document.Add(new Paragraph($"Користувач: {username}", font));

            document.Close();
            writer.Close();
            Console.WriteLine($"Base directory: {AppDomain.CurrentDomain.BaseDirectory}");
            return ms.ToArray();
        }
    }
}
