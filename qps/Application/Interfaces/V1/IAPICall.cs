using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.V1
{
    public interface IAPICall
    {
        public Task<RepT> CallingAPI<RepT, ReqT>(string ApiName, ReqT requestBody);
        public Task<RepT> CallingSAPAPI<RepT, ReqT>( ReqT requestBody, ApiData apiData);
    }
}
