using Domain.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class GetReportReq
    {
        public List<AppsBsr> reportData { get; set; } = new();
        public string? reportFilter { get; set; }
        public string? reportType { get; set; }
        public int ImageDownloadLimit { get; set; }
    }
}
