using Application.Interfaces.V1;
using Dapper;
using Domain.Entities.Base;
using Domain.Entities.Common;
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
    public class RoleService : IRole
    {
        private readonly DapperHelper _dapperHelper;
        public RoleService(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }

        public async Task<InUpRes> InsertUpdateRole(RoleMaster req)
        {
            var res = new InUpRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@Role_Name", req.Role_Name, DbType.String);
            parameters.Add("@Menu", req.Menu, DbType.Boolean);
            parameters.Add("@CMS", req.CMS, DbType.Boolean);
            parameters.Add("@Media", req.Media, DbType.Boolean);
            parameters.Add("@User_Management", req.User_Management, DbType.Boolean);
            parameters.Add("@Active_Status", req.Active_Status, DbType.Boolean);
            parameters.Add("@Inserted_Updated_By", req.Inserted_Updated_By, DbType.Int32);
            //Output parameters
            parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("Insert_Update_RoleMaster", parameters);
            if (!parameters.Get<int>("Error_Code").Equals(0))
            {
                res.responseCode = parameters.Get<int>("Error_Code");
                res.responseMessage = parameters.Get<string>("Error_Message");
            }
            return res;
        }
        public async Task<List<RoleMaster>> SelectAllRoles(SelectListReq req)
        {
            var res = new List<RoleMaster>();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@StrField", req.StrField, DbType.String);
            parameters.Add("@Cmd", req.Cmd, DbType.String);
            var result = await _dapperHelper.ExecuteStoredProcedureListAsync<RoleMaster>("Select_SelectAll_SelectList", parameters);
            if (result.ToList().Count > 0)
            {
                res = result.ToList();
            }
            return res;
            //var res = new List<RoleMaster>();
            //var parameters = new DynamicParameters();
            //// Input parameters
            //parameters.Add("@Id", req.Id, DbType.Int32);
            //parameters.Add("@Cmd", req.Cmd, DbType.String);
            //var resultSet = await _dapperHelper.ExecuteStoredProcedureMultipleListAsync<RoleMaster, RoleDetails>("Select_SelectAll_SelectList", parameters);
            //if (resultSet.Item1.ToList().Count > 0 && resultSet.Item2.ToList().Count == 0)
            //{
            //    res = resultSet.Item1.ToList();
            //}
            //else if(resultSet.Item1.ToList().Count>0 && resultSet.Item2.ToList().Count > 0)
            //{
            //    foreach(var Item in resultSet.Item1.ToList())
            //    {
            //        var RoleDetails = resultSet.Item2.ToList().Where(rd => rd.Role_Id == Item.Id);
            //        if (RoleDetails != null)
            //        {
            //            Item.RoleDetailsList = RoleDetails.ToList();
            //        }
            //        res.Add(Item);
            //    }
            //}
            //return res;
        }
        public async Task<List<RoleDetails>> RoleDetailsOnId(SelectListReq req)
        {
            var res = new List<RoleDetails>();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@StrField", req.StrField, DbType.String);
            parameters.Add("@Cmd", req.Cmd, DbType.String);
            var result = await _dapperHelper.ExecuteStoredProcedureListAsync<RoleDetails>("Select_SelectAll_SelectList", parameters);
            if (result.ToList().Count > 0)
            {
                res = result.ToList();
            }
            return res;
        }
        public async Task<List<RoleList>> SelectRoleList(SelectListReq req)
        {
            var res = new List<RoleList>();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@StrField", req.StrField, DbType.String);
            parameters.Add("@Cmd", req.Cmd, DbType.String);
            var Res = await _dapperHelper.ExecuteStoredProcedureListAsync<RoleList>("Insert_Update_UserMaster", null);
            res = Res.ToList();
            return res;
        }
    }
}
