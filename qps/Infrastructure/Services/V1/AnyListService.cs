using BSRApplication.Interfaces.V1;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Infrastructure.Utilitys;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.V1
{
    public class AnyListService : IAnyList
    {
        private readonly DapperHelper _apperHelper;
        public AnyListService(DapperHelper dapperHelper)
        {
            _apperHelper = dapperHelper;
        }
        public async Task<GetListRes> GetList(GetAnyListReq req)
        {
            GetListRes res = new();
            var result = await _apperHelper.ExecuteStoredProcedureListAsync<ListItem>("PRO_GET_LIST_BASED_ON_CMD", new { CMD = req.CMD, STRING_VAR1 = req.STRING_VAR1, STRING_VAR2 = req.STRING_VAR2 });
            res.listItems = result!.ToList();
            return res;
        }
    }
}
