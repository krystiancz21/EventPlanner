using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Interfaces;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Microsoft.Extensions.Configuration;

namespace EventPlanner.Infrastructure.Services;

public class PdfCertificateGenerator(IConfiguration configuration) : ICertificateGenerator
{
    private readonly string _certificateDirectory = configuration["CertificateSettings:Directory"] ?? "certificates";

    public async Task<string> GenerateCertificateAsync(Certificate certificate, Workshop workshop, User user)
    {
        // Inicjalizacja QuestPDF
        QuestPDF.Settings.License = LicenseType.Community;

        // Upewnij się, że katalog istnieje
        Directory.CreateDirectory(_certificateDirectory);

        // Generowanie nazwy pliku
        string fileName = $"{user.UserName}_{workshop.Title}_{certificate.Id}.pdf";
        string filePath = Path.Combine(_certificateDirectory, fileName); // Użyj ścieżki z konfiguracji

        // Generowanie dokumentu PDF
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .AlignCenter()
                    .Text("Certificate of Completion")
                    .SemiBold().FontSize(36).FontColor(Colors.Blue.Darken2);

                page.Content()
                    .PaddingVertical(5, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Item().Text($"This certificate is proudly presented to:").FontSize(22).Italic().FontColor(Colors.Grey.Darken2).AlignCenter();
                        column.Item().PaddingTop(0.5f, Unit.Centimetre).Text($"{user.UserName}").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium).AlignCenter();
                        column.Item().PaddingTop(2, Unit.Centimetre).Text($"In recognition of the successful completion of:").FontSize(22).Italic().FontColor(Colors.Grey.Darken2).AlignCenter();
                        column.Item().Text($"{workshop.Title}").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium).AlignCenter();
                        column.Item().PaddingTop(4, Unit.Centimetre).Row(row =>
                        {
                            row.RelativeItem().Column(column =>
                            {
                                column.Item().Text($"Date: {workshop.Date.ToShortDateString()}").FontSize(18).Italic().FontColor(Colors.Grey.Darken2).AlignCenter();
                                column.Item().PaddingTop(1, Unit.Centimetre).Text("Signature: ___________").FontSize(18).Italic().FontColor(Colors.Grey.Darken2).AlignCenter();
                            });
                        });
                    });
            });
        });

        // Zapis dokumentu do pliku
         document.GeneratePdf(filePath);

        // Zwracanie ścieżki do pliku
        return filePath;
    }
}
