using Domain.Entities.Request;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.V1
{
    public interface IAppsBsr
    {
        public Task<AppsBsrRes> GetAppsBsr(GetBSRDataReq req);
    }
}
