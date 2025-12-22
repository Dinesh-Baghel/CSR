using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entities.Modals
{
    public class BSRSettings
    {
        public int StartYear { get; set; }
        public int ImageDownloadLimit { get; set; }
        public int PageSize { get; set; }
        public int AdminPageSize { get; set; }
        public int FirstDayOfWeek { get; set; }
        public string? DefaultSortColumns { get; set; }
        public bool sortAscending { get; set; }
        public string? Environment { get; set; }
    }

    public class EmailServerSettings
    {
        public string SmtpServer { get; set; } = string.Empty;
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPwd { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public bool SmtpDefaultCredential { get; set; }
        public bool EnableSsl { get; set; } = false;
        public string From { get; set; } = string.Empty;
    }


}
