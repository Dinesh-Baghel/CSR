using Domain.Entities.Modals;

namespace BSRApi.Interfaces
{
    public interface IReporting
    {
        Task<byte[]> GenerateBsrListExcelReport(List<AppsBsr> data);
        Task<byte[]> GenerateBsrListExcelReportWithImage(List<AppsBsr> data);
        Task<byte[]> GenerateBsrListPdfReport(List<AppsBsr> data);
        Task<byte[]> GenerateBsrListPdfReportWithImage(List<AppsBsr> data);
    }
}
