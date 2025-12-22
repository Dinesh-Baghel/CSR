using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class ItemDetailsReq
    {
        public string? barCode { get; set; }
        public string? storeCode { get; set; } = "";
    }
}
