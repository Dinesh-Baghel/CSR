using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class SAP_API_DATA_Req
    {
        public string? TaskName { get; set; }
        public string? Param { get; set; } = string.Empty;
    }
}
