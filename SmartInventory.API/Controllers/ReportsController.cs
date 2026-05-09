using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.Interfaces.Service_Interfaces.Report_Interface;
using SmartInventory.Application.Services;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("excel")]
        public async Task<IActionResult> ExportExcel()
        {
            var response = await _reportService.ExportSalesToExcel();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return File(
                response.Data!,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "SalesReport.xlsx"
            );
        }

        [HttpGet("pdf")]
        public async Task<IActionResult> ExportPdf()
        {
            var response = await _reportService.ExportSalesToPdf();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return File(
                response.Data!,
                "application/pdf",
                "SalesReport.pdf"
            );
        }
    }
}
