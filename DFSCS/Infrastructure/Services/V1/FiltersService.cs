using Application.Interfaces.V1;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Infrastructure.Utilitys;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.V1
{
    public class FiltersService : IFilters
    {
        private readonly DapperHelper _dapperHelper;
        public FiltersService(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async Task<Dictionary<string, List<string>>> GetDisplayFilters(GetDisplayFiltersReq req)
        {
            var result = await _dapperHelper.QueryFirstOrDefaultAsync<dynamic>("Sp_GetBSRFilterValues", new { YearWeek = req.yearWeek, userId=req.userId });
            var filters = new Dictionary<string, List<string>>();
            var dict = result as IDictionary<string, object>;
            if (dict != null)
            {
                foreach (var kv in dict)
                {
                    if (kv.Value is string json && !string.IsNullOrWhiteSpace(json))
                    {
                        var values = JsonConvert.DeserializeObject<List<FilterValue>>(json);
                        filters[kv.Key] = values!.Select(v => v.value).ToList()!;
                    }
                }
            }
            return filters;
        }
    }
}
