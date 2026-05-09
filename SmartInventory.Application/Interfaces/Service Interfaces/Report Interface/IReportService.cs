using SmartInventory.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Service_Interfaces.Report_Interface
{
    public interface IReportService
    {
        Task<ServiceResponse<byte[]>> ExportSalesToExcel();
        Task<ServiceResponse<byte[]>> ExportSalesToPdf();

    }
}
