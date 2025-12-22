using BSRApi.Services;
using BSRApi.Interfaces;
using Domain.Entities.Modals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace QPSApi.Controllers.V1
{
    [Route("api/v1/[Action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReporting _report;

        private readonly ILogger<ReportsController> _logger;
        public ReportsController(IReporting report, ILogger<ReportsController> logger)
        {
            _report = report;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> GetBsrListExcelReport(List<AppsBsr> data)
        {
            try
            {
                
                if (data == null || data.Count == 0)
                    return BadRequest("No data received to generate report.");
                var res = await _report.GenerateBsrListExcelReport(data);
                if (res != null)
                {
                    _logger.LogError("Controller  Fine");
                    //return File(res, "application/pdf", $"BSR_Report_{DateTime.Now:yyyyMMddHHmmss}.pdf");
                    return File(res, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AppsBsrReport.xlsx");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetBsrListExcelReportWithImage(List<AppsBsr> data)
        {
            if (data == null || data.Count == 0)
                return BadRequest("No data received to generate report.");
            var res = await _report.GenerateBsrListExcelReportWithImage(data);
            if (res != null)
            {
                //return File(res, "application/pdf", $"BSR_Report_{DateTime.Now:yyyyMMddHHmmss}.pdf");
                return File(res, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AppsBsrReport.xlsx");
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> GetBsrListPdfReport(List<AppsBsr> data)
        {
            if (data == null || data.Count == 0)
                return BadRequest("No data received to generate report.");
            var res = await _report.GenerateBsrListPdfReport(data);
            if (res != null)
            {
                return File(res, "application/pdf", $"BSR_Report_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> GetBsrListPdfReportWithImage(List<AppsBsr> data)
        {
            if (data == null || data.Count == 0)
                return BadRequest("No data received to generate report.");
            var res = await _report.GenerateBsrListPdfReportWithImage(data);
            if (res != null)
            {
                return File(res, "application/pdf", $"BSR_Report_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            }
            return BadRequest();
        }
    }
}
