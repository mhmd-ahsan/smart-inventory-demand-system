using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Sale_Interface;
using SmartInventory.Application.Interfaces.Service_Interfaces.Report_Interface;
using OfficeOpenXml;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace SmartInventory.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly ISaleRepository _repo;

        public ReportService(ISaleRepository repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponse<byte[]>> ExportSalesToExcel()
        {
            try
            {
                var sales = await _repo.GetAllSalesAsync();

                // License context is now handled in Program.cs globally.
                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Sales Report");

                // Headers
                worksheet.Cells[1, 1].Value = "Product Name";
                worksheet.Cells[1, 2].Value = "Quantity Sold";
                worksheet.Cells[1, 3].Value = "Total Revenue";

                // Basic Header Styling
                using (var range = worksheet.Cells[1, 1, 1, 3])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                int row = 2;
                foreach (var sale in sales)
                {
                    // FIX: Null-safe check for Product
                    worksheet.Cells[row, 1].Value = sale.Product?.Name ?? "N/A";
                    worksheet.Cells[row, 2].Value = sale.Quantity;
                    worksheet.Cells[row, 3].Value = sale.TotalPrice;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();
                var fileBytes = package.GetAsByteArray();

                return ServiceResponse<byte[]>.SuccessResponse(fileBytes, "Excel report generated successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<byte[]>.FailureResponse("Failed to generate Excel report.", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResponse<byte[]>> ExportSalesToPdf()
        {
            try
            {
                var sales = await _repo.GetAllSalesAsync();
                using var stream = new MemoryStream();
                var writer = new PdfWriter(stream);
                using var pdf = new PdfDocument(writer);
                using var document = new iText.Layout.Document(pdf);

                // Add Title
                document.Add(new Paragraph("Sales Report")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold());

                // Create a Table for PDF (Better than plain paragraphs)
                Table table = new Table(UnitValue.CreatePercentArray(new float[] { 50, 25, 25 })).UseAllAvailableWidth();
                table.AddHeaderCell("Product");
                table.AddHeaderCell("Quantity");
                table.AddHeaderCell("Revenue");

                foreach (var sale in sales)
                {
                    table.AddCell(sale.Product?.Name ?? "N/A");
                    table.AddCell(sale.Quantity.ToString());
                    table.AddCell(sale.TotalPrice.ToString("C")); // Formats as Currency
                }

                document.Add(table);
                document.Close();

                return ServiceResponse<byte[]>.SuccessResponse(stream.ToArray(), "PDF report generated successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<byte[]>.FailureResponse("Failed to generate PDF report.", new List<string> { ex.Message });
            }
        }
    }
}