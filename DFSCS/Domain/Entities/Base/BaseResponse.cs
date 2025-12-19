using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public abstract class BaseResponse
    {
        public int responseCode { get; set; } = 0;
        public string? responseMessage { get; set; } = "SUCCESS";
    }
}
