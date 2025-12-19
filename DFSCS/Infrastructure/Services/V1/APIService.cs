using Application.Interfaces.V1;
using Dapper;
using Domain.Entities.Common;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Infrastructure.Utilitys;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.V1
{
    public class APIService : IAPI
    {
        private readonly DapperHelper _dapperHelper;
        public APIService(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<APIRes> GetAllApi()
        {
            var APIRes = new APIRes();
            var parameters = new DynamicParameters();
            // Output parameters
            //parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var res = await _dapperHelper.ExecuteStoredProcedureListAsync<ApiData>("Get_All_Api", parameters);
            if (res.ToList().Count == 0)
            {
                APIRes.responseCode = 1;
                APIRes.responseMessage = "No data found";
                //parameters.Get<string>("Error_Message")
            }
            APIRes.ApiData = res.ToList();
            return APIRes;
        }
    }
}
