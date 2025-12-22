using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class GetDisplayFiltersReq
    {
        public int userId { get; set; }
        public string? WhereClause { get; set; } = "";
        public string? RequiredColNames { get; set; } = "All";
        public string? SEASON { get; set; } = "All";
    }
}
