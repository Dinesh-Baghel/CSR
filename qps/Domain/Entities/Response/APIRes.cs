using Domain.Entities.Base;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class APIRes : BaseResponse
    {
        public List<ApiData>? ApiData { get; set; }
    }
}
