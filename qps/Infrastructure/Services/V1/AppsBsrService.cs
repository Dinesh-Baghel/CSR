using Application.Interfaces.V1;
using Dapper;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Infrastructure.Utilitys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.V1
{
    public class AppsBsrService : IAppsBsr
    {
        private readonly DapperHelper _dapperHelper;
        public AppsBsrService(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async Task<AppsBsrRes> GetAppsBsr(GetBSRDataReq req)
        {
            var res = new AppsBsrRes();
            var parms = new DynamicParameters();
            // Input parameters
            parms.Add("@YearWeek", req.yearWeek, DbType.String);
            parms.Add("@WhereClause", req.whereClause, DbType.String);
            parms.Add("@OrderByClause", req.OrderByClause, DbType.String);
            parms.Add("@PageNumber", req.pageNumber, DbType.Int32);
            parms.Add("@PageSize", req.pageSize, DbType.Int32);
            parms.Add("@userId", req.userId, DbType.Int32);
            var result = await _dapperHelper.ExecuteStoredProcedureListAsync<AppsBsr>("Sp_GetBSRData", parms);
            if (result.ToList().Count > 0)
            {
                res.APPS_BSR = result.ToList();
                res.responseCode = 0;
                res.responseMessage = "SUCCESS";
            }
            else
            {
                res.responseCode = 1;
                res.responseMessage = "Bsr records not found.";
            }
            return res;
        }
    }
}
