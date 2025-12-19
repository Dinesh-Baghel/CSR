using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class MenuReq
    {
        public string? empCode {  get; set; }
        public string? menuURL { get; set; }
        public string? menuName { get; set; }
        public string? platform { get; set; }
    }
}
