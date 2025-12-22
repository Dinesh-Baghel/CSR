using Domain.Entities.Base;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class ApiMasterRes : BaseResponse
    {
        public bool UpdatedApplication { get; set; }
        public bool IsCashCollection { get; set; }
        public bool IsRatingVisible { get; set; }
        public bool IsGvAvail { get; set; }
        public bool IsUpiAvail { get; set; }
        public List<ApiData>? ApiData { get; set; }
    }
}
