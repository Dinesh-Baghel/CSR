using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class SelectListReq
    {
        public int Id { get; set; }
        public string? Cmd {  get; set; }
        public string? StrField { get; set; } = string.Empty;
        public string? ResponseType { get; set; }
    }
}
