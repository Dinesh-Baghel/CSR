using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class StoreDetailsRequest
    {
        public string? storeCode { get; set; }
        public string? lattitude { get; set; } = "0";
        public string? longitude { get; set; } = "0";
    }
}
