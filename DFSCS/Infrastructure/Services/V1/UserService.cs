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
    public class UserService : IUser
    {
        private readonly DapperHelper _dapperHelper;
        public UserService(DapperHelper dapperHelper)
        {  
            _dapperHelper = dapperHelper;
        }

        public async Task<CopLoginRes> CopLogin(CopLoginReq req)
        {
            var Res = new CopLoginRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.UserDetails.Id, DbType.Int32);
            parameters.Add("@Emp_Code", req.UserDetails.Emp_Code, DbType.String);
            parameters.Add("@Emp_Name", req.UserDetails.Emp_Name, DbType.String);
            parameters.Add("@Emp_Location", req.UserDetails.Emp_Location, DbType.String);
            parameters.Add("@Emp_Loc_Code", req.UserDetails.Emp_Loc_Code, DbType.String);
            parameters.Add("@Emp_Dept_Name", req.UserDetails.Emp_Dept_Name, DbType.String);
            parameters.Add("@Emp_Desig", req.UserDetails.Emp_Desig, DbType.String);
            parameters.Add("@Emp_Contact_No", req.UserDetails.Emp_Contact_No, DbType.String);
            parameters.Add("@Emp_Mail_Id", req.UserDetails.Emp_Mail_Id, DbType.String);
            parameters.Add("@Emp_Rpt_Mng", req.UserDetails.Emp_Rpt_Mng, DbType.String);
            parameters.Add("@Is_Resign", req.UserDetails.Is_Resign, DbType.Boolean);
            parameters.Add("@Emp_Grade", req.UserDetails.Emp_Grade, DbType.Int32);
            parameters.Add("@Emp_Grade_Desc", req.UserDetails.Emp_Grade_Desc, DbType.String);
            parameters.Add("@Emp_Status", req.UserDetails.Emp_Status, DbType.Boolean);
            parameters.Add("@Ip_Address", req.UserDetails.Ip_Address, DbType.String);
            parameters.Add("@Role_Id", req.UserDetails.Role_Id, DbType.Int32);
            parameters.Add("@Inserted_Updated_By", req.Inserted_Updated_By, DbType.Int32);
            //Output parameters
            parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet = await _dapperHelper.ExecuteStoredProcedureMultiple3ListAsync<UserDetails, Modules, Menu>("User_Login", parameters);
            if(!parameters.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = parameters.Get<int>("Error_Code");
                Res.responseMessage = parameters.Get<string>("Error_Message");
            }
            Res.UserDetails = ResSet.Item1.ToList().Count == 0 ? new UserDetails() : ResSet.Item1.ToList()[0];
            Res.Modules = ResSet.Item2.ToList().Count == 0 ? new Modules() : ResSet.Item2.ToList()[0];
            Res.MenuList = ResSet.Item3.ToList();
            return Res;
        }
        public async Task<GenRes> SetUser(UserDetails req)
        {
            var Res = new GenRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@Emp_Code", req.Emp_Code, DbType.String);
            parameters.Add("@Emp_Name", req.Emp_Name, DbType.String);
            parameters.Add("@Emp_Location", req.Emp_Location, DbType.String);
            parameters.Add("@Emp_Loc_Code", req.Emp_Loc_Code, DbType.String);
            parameters.Add("@Emp_Dept_Name", req.Emp_Dept_Name, DbType.String);
            parameters.Add("@Emp_Desig", req.Emp_Desig, DbType.String);
            parameters.Add("@Emp_Contact_No", req.Emp_Contact_No, DbType.String);
            parameters.Add("@Emp_Mail_Id", req.Emp_Mail_Id, DbType.String);
            parameters.Add("@Emp_Rpt_Mng", req.Emp_Rpt_Mng, DbType.String);
            parameters.Add("@Is_Resign", req.Is_Resign, DbType.Boolean);
            parameters.Add("@Emp_Grade", req.Emp_Grade, DbType.Int32);
            parameters.Add("@Emp_Grade_Desc", req.Emp_Grade_Desc, DbType.String);
            parameters.Add("@Emp_Status", req.Emp_Status, DbType.Boolean);
            parameters.Add("@Ip_Address", req.Ip_Address, DbType.String);
            parameters.Add("@Role_Id", req.Role_Id, DbType.Int32);
            parameters.Add("@Inserted_Updated_By", req.Inserted_Updated_By, DbType.Int32);
            // Table-valued parameter
            var roleDetailsTable = new DataTable();
            roleDetailsTable.Columns.Add("Menu_Id", typeof(int));
            roleDetailsTable.Columns.Add("Permission", typeof(string));
            foreach (var role in req.RoleDetailsList)
            {
                roleDetailsTable.Rows.Add(role.Menu_Id, role.Permission);
            }
            parameters.Add("@RoleDetails", roleDetailsTable.AsTableValuedParameter("RoleDetails"));

            //Output parameters
            parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet = await _dapperHelper.ExecuteStoredProcedureAsync("Insert_Update_UserMaster", parameters);
            if (!parameters.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = parameters.Get<int>("Error_Code");
                Res.responseMessage = parameters.Get<string>("Error_Message");
            }
            return Res;
        }
        public async Task<UserDetails> GetUserOnEmpCode(SelectListReq req)
        {
            var Res = new UserDetails();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", 0, DbType.Int32);
            parameters.Add("@StrField", req.StrField, DbType.String);
            parameters.Add("@Cmd", req.Cmd, DbType.String);
            var OutSet = await _dapperHelper.ExecuteStoredProcedureMultipleListAsync<UserDetails, RoleDetails>("Select_SelectAll_SelectList", parameters);
            Res = OutSet.Item1.ToList()[0];
            Res.RoleDetailsList = OutSet.Item2.ToList();
            return Res;
        }
        public async Task<List<UserDetails>> GetAllUsers()
        {
            var Res = new List<UserDetails>();
            //var parameters = new DynamicParameters();
            //parameters.Add("@Id", 0, DbType.Int32);
            //parameters.Add("@StrField", "", DbType.String);
            //parameters.Add("@Cmd", "Get_All_Users", DbType.String);
            var parameters = new
            {
                Id = 0,
                StrField = "",
                Cmd = "Get_All_Users"
            };
            var Out = await _dapperHelper.ExecuteStoredProcedureListAsync<UserDetails>("Select_SelectAll_SelectList", parameters);
            Res = Out.ToList();
            return Res;
        }
        public async Task<CopLoginRes> GetLoggedInUserOnEmpCode(SelectListReq req)
        {
            var Res = new CopLoginRes();
            var parameters = new DynamicParameters();
            parameters.Add("@Id", 0, DbType.Int32);
            parameters.Add("@StrField", req.StrField, DbType.String);
            parameters.Add("@Cmd", "Get_LoggedinUser_Details_On_Emp_Code", DbType.String);
            //var OutSet = await _dapperHelper.ExecuteStoredProcedureMultipleListAsync<UserDetails, RoleDetails>("Select_SelectAll_SelectList", parameters);
            //Res = OutSet.Item1.ToList()[0];
            //Res.RoleDetailsList = OutSet.Item2.ToList();
            var ResSet = await _dapperHelper.ExecuteStoredProcedureMultiple3ListAsync<UserDetails, Modules, Menu>("Select_SelectAll_SelectList", parameters);
            if (ResSet.Item1.ToList().Count == 0)
            {
                Res.responseCode = 1;
                Res.responseMessage = "Data not found.";
            }
            Res.UserDetails = ResSet.Item1.ToList().Count == 0 ? new UserDetails() : ResSet.Item1.ToList()[0];
            Res.Modules = ResSet.Item2.ToList().Count == 0 ? new Modules() : ResSet.Item2.ToList()[0];
            Res.MenuList = ResSet.Item3.ToList();
            return Res;
        }
    }
}
