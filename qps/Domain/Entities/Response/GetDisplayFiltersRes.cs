using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class GetDisplayFiltersRes : BaseResponse
    {
        public Dictionary<string, List<string>> filters { get; set; } = new();
    }
}
