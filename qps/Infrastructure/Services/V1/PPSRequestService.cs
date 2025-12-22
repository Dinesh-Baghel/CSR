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
    public class PPSRequestService :IPPSRequest
    {
        private readonly DapperHelper _dapperHelper;

        public PPSRequestService(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async Task<InUpRes> CreatePPSRequest(PpsRequest req)
        {
            var res = new InUpRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@REQUEST_ID", req.Request_Id, DbType.Int32);
            parameters.Add("@BUYER_ID", req.Buyer_Id, DbType.String);
            parameters.Add("@GENERIC_ARTICLE", req.Generic_Article, DbType.String);
            parameters.Add("@ARTICLE_DESCRIPTION", req.Article_Description, DbType.String);
            parameters.Add("@COLOR", req.Color, DbType.String);
            parameters.Add("@MC", req.Mc, DbType.String);
            parameters.Add("@MC_DESCRIPTION", req.Mc_Description, DbType.String);
            parameters.Add("@DIVISION", req.Division, DbType.String);
            parameters.Add("@DIVISION_DESCRIPTION", req.Division_Description, DbType.String);
            parameters.Add("@VENDOR_CODE", req.Vendor_Code, DbType.String);
            parameters.Add("@VENDOR_NAME", req.Vendor_Name, DbType.String);
            parameters.Add("@PP_TYPE", req.PP_Type, DbType.String);
            parameters.Add("@QTY", req.Qty, DbType.Int32);
            parameters.Add("@ATTACHMENTS", req.ATTACHMENTS, DbType.String);
            parameters.Add("@MRP", req.Pps_Mrp, DbType.Decimal);
            //Output parameters 
            parameters.Add("@ERROR_CODE", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ERROR_MESSAGE", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            
            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("QPS_PPS_REQUEST_INSERT_UPDATE", parameters);
            if (!parameters.Get<int>("ERROR_CODE").Equals(0))
            {
                res.responseCode = parameters.Get<int>("ERROR_CODE");
                res.responseMessage = parameters.Get<string>("ERROR_MESSAGE");
            }
            return res;
        }

        public async Task<List<T>> SelectAllAsync<T>(SelectListReq req) where T : class
        {
            var parameters = new DynamicParameters();

            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@StrField", req.StrField, DbType.String);
            parameters.Add("@Cmd", req.Cmd, DbType.String);

            var result = await _dapperHelper.ExecuteStoredProcedureListAsync<T>("QPS_QUERY_PROC", parameters);

            return result?.ToList() ?? new List<T>();
        }

        public async Task<InUpRes> UpdateStatusPPSRequest(UpdateStausPpsRequest req)
        {
            var res = new InUpRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@REQUEST_ID", req.Request_Id, DbType.Int32);
            parameters.Add("@VERIFIED_BY", req.User_Id, DbType.String);
            parameters.Add("@STATUS_ID", req.StatusId, DbType.Int32);
            parameters.Add("@COMMENTS", req.Comments, DbType.String);
            parameters.Add("@ATTACHMENTS", req.ATTACHMENTS, DbType.String);
            //Output parameters
            parameters.Add("@ERROR_CODE", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ERROR_MESSAGE", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);

            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("QPS_PPS_LOG_UPDATE", parameters);
            if (!parameters.Get<int>("ERROR_CODE").Equals(0))
            {
                res.responseCode = parameters.Get<int>("ERROR_CODE");
                res.responseMessage = parameters.Get<string>("ERROR_MESSAGE");
            }
            return res;
        }
    }
}
