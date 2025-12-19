using Domain.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.V1
{
    public interface IFilters
    {
        public Task<Dictionary<string, List<string>>> GetDisplayFilters(GetDisplayFiltersReq req);
    }
}
