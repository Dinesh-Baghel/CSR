using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class SetFlagsReq
    {
        public int Id { get; set; }
        public bool Bool_Flag { get; set; }
        public int Int_Flag { get; set; }
        public string? Str_Flag { get; set; }
        public int Updated_By { get; set; }
        public string? Cmd {  get; set; }
    }
}
