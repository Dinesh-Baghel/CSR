using BSRApi.Interfaces;
using Domain.Entities.Modals;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Text.Json;

namespace BSRApi.Services
{
    public class ReportingService : IReporting
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ReportingService> _logger;
        public ReportingService(IWebHostEnvironment env, ILogger<ReportingService> logger)
        {
            _env = env;
            _logger = logger;
        }
        public async Task<byte[]> GenerateBsrListExcelReport(List<AppsBsr> data)
        {
            try
            {
                //var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "BsrListReport.rdlc");
                var reportPath = Path.Combine(_env.ContentRootPath, "Reports", "BsrListReport.rdlc");
                using var report = new LocalReport();
                report.ReportPath = reportPath;

                // ✅ Enable external images
                report.EnableExternalImages = true;
                report.EnableHyperlinks = true;

                // Convert to DataTable
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var table = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

                report.DataSources.Add(new ReportDataSource("BsrReportDataSet", table));

                // Still sync, but method is async for future proofing
                //return report.Render("PDF");
                _logger.LogError("Reporting Service  Fine");
                return report.Render("EXCELOPENXML");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Detailed error during RDLC report rendering inside GenerateBsrListExcelReport.");
                return null;
            }
        }
        public async Task<byte[]> GenerateBsrListExcelReportWithImage(List<AppsBsr> data)
        {
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "BsrListReportWithImage.rdlc");

            using var report = new LocalReport();
            report.ReportPath = reportPath;

            // ✅ Enable external images
            report.EnableExternalImages = true;
            report.EnableHyperlinks = true;

            // Convert to DataTable
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var table = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

            report.DataSources.Add(new ReportDataSource("BsrReportDataSet", table));

            // Still sync, but method is async for future proofing
            //return report.Render("PDF");
            return report.Render("EXCELOPENXML");
        }
        public async Task<byte[]> GenerateBsrListPdfReport(List<AppsBsr> data)
        {
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "BsrListReport.rdlc");

            using var report = new LocalReport();
            report.ReportPath = reportPath;

            // ✅ Enable external images
            report.EnableExternalImages = true;
            report.EnableHyperlinks = true;

            // Convert to DataTable
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var table = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

            report.DataSources.Add(new ReportDataSource("BsrReportDataSet", table));

            // Still sync, but method is async for future proofing
            return report.Render("PDF");
        }
        public async Task<byte[]> GenerateBsrListPdfReportWithImage(List<AppsBsr> data)
        {
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "BsrListReportWithImage.rdlc");

            using var report = new LocalReport();
            report.ReportPath = reportPath;

            // ✅ Enable external images
            report.EnableExternalImages = true;
            report.EnableHyperlinks = true;

            // Convert to DataTable
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            var table = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(json);

            report.DataSources.Add(new ReportDataSource("BsrReportDataSet", table));

            // Still sync, but method is async for future proofing
            return report.Render("PDF");
        }
    }
}
