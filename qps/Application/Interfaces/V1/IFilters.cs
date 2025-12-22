using Domain.Entities.Request;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSRApplication.Interfaces.V1
{
    public interface IFilters
    {
        public Task<GetDisplayFiltersRes> GetDisplayFilters(GetDisplayFiltersReq req);
    }
}
