using Application.Interfaces.V1;
using Dapper;
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
    public class SetFlagsService:ISetFlags
    {
        private readonly DapperHelper _DapperHelper;
        public SetFlagsService(DapperHelper dapperHelper)
        {
            _DapperHelper = dapperHelper;
        }

        public async Task<InUpRes> SetFlags(SetFlagsReq req)
        {
            var Res = new InUpRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@Bool_Flag", req.Bool_Flag, DbType.Boolean);
            parameters.Add("@Int_Flag", req.Int_Flag, DbType.Int32);
            parameters.Add("@Str_Flag", req.Str_Flag, DbType.String);
            parameters.Add("@Updated_By", req.Updated_By, DbType.Int32);
            parameters.Add("@Cmd", req.Cmd, DbType.String);
            //Output parameters
            parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet = await _DapperHelper.ExecuteStoredProcedureAsync("Set_Flags", parameters);
            if (!parameters!.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = parameters.Get<int>("Error_Code");
                Res.responseMessage = parameters.Get<string>("Error_Message");
            }
            return Res;
        }
    }
}
