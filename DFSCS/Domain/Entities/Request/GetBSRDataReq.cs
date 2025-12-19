using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class GetBSRDataReq
    {
        public string? yearWeek { get; set; }
        public string whereClause { get; set; } = string.Empty;
        public string OrderByClause { get; set; } = string.Empty;
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 20;
        public int userId { get; set; }
    }
}
