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
    }
}
