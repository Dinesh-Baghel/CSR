using Domain.Entities.Common;
using Domain.Entities.Request;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.V1
{
    public interface IAPI
    {
        public Task<APIRes> GetAllApi();
        public Task<ApiData> GetSpecificApiData(SAP_API_DATA_Req req);
    }
}
