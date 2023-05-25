using CourseOPR.Database;
using CourseOPR.Models;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore.Query;

namespace CourseOPR.Servises
{
    public class PdfCreator
    {

        public byte[] CreatePdf(List<Score> scores )
        {
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer.SetSmartMode(true));
            Document document = new Document(pdf, PageSize.A4);
            var font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");
            document.SetFont(font);
            Paragraph header = new Paragraph("Приложение к диплому А №132123")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10);
            document.Add(header);
            document.Add(new Paragraph("ВЫПИСКА").SetTextAlignment(TextAlignment.CENTER).SetFontSize(14).SetBold());
            document.Add(new Paragraph("из зачетно-экзаменационной ведомости").SetTextAlignment(TextAlignment.CENTER).SetFontSize(14));
            document.Add(new Paragraph("(без диплома не действительна)").SetTextAlignment(TextAlignment.CENTER).SetFontSize(14));
            var table = new Table(new float[] { 4, 1, 2 });
            table.SetWidth(UnitValue.CreatePercentValue(100));
            table.AddHeaderCell(
                new Cell().Add(
                    new Paragraph("dfgdfs")));
            table.AddHeaderCell(
                    new Cell().Add(
                    new Paragraph("hours")));
            table.AddHeaderCell(
                    new Cell().Add(
                    new Paragraph("fwe")));
            foreach (var score in scores)
            {
                table.AddCell(
                    new Cell().Add(
                        new Paragraph(score.Subject.SubjectName)));
                table.AddCell(
                    new Cell().Add(
                        new Paragraph(score.Subject.Hours.ToString())));
                table.AddCell(
                    new Cell().Add(
                        new Paragraph(score.ScoreValue)));
            }


            document.Add(table);
            


            document.Close();

            return stream.ToArray();
        }
    }
}
