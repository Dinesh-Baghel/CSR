using BSRApplication.Interfaces.V1;
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
    public class MenuService : IMenu
    {
        private readonly DapperHelper _dapperHelper;
        public MenuService(DapperHelper dapperHelper)
        {
            _dapperHelper = dapperHelper;
        }
        public async Task<MenuRes> GetAllMenus(SelectListReq req)
        {
            MenuRes res = new();
            var parms = new DynamicParameters();
            // Input parameters
            parms.Add("@Id", req.Id, DbType.Int32);
            parms.Add("@StrField", req.StrField, DbType.String);
            parms.Add("@Cmd", req.Cmd, DbType.String);
            //Output parameters
            //parms.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parms.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet = await _dapperHelper.ExecuteStoredProcedureMultipleListAsync<Menu, RoleList>("PRO_SELECT_SELECTALL_SELECTLIST", parms);
            if (ResSet.Item1.ToList().Count > 0)
                {
                    if (ResSet.Item2.ToList().Count > 0)
                    {
                        foreach (var item in ResSet.Item1.ToList())
                        {
                            item.RoleList = ResSet.Item2.ToList();
                            res.menuList!.Add(item);
                        }
                    }
                    else
                    {
                        res.menuList = ResSet.Item1.ToList();
                    }
                }
                return res;
        }
        public async Task<GetMenuListRes> GetMenuList(SelectListReq req)
        {
            var res = new GetMenuListRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@StrField", req.StrField, DbType.String);
            parameters.Add("@Cmd", req.Cmd, DbType.String);
            //Output parameters
            //parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var result = await _dapperHelper.ExecuteStoredProcedureListAsync<MenusList>("PRO_SELECT_SELECTALL_SELECTLIST", parameters);
            if (result.ToList().Count > 0)
            {
                res.menusLists = result.ToList();
            }
            return res;
        }
        public async Task<InUpRes> SetMenu(Menu req)
        {
            var Res = new InUpRes();
            var parameters = new DynamicParameters();
            // Input parameters
            parameters.Add("@Id", req.Id, DbType.Int32);
            parameters.Add("@Label", req.Label, DbType.String);
            parameters.Add("@Url", req.Url, DbType.String);
            parameters.Add("@Order", req.Order, DbType.Int32);
            parameters.Add("@Parent_Id", req.Parent_Id, DbType.Int32);
            parameters.Add("@Website_Visibility", req.Website_Visibility, DbType.Boolean);
            parameters.Add("@Active_Status", req.Active_Status, DbType.Boolean);
            parameters.Add("@Page_Title", req.Page_Title, DbType.String);
            parameters.Add("@Page_Description", req.Page_Description, DbType.String);
            parameters.Add("@Page_Keywords", req.Page_Keywords, DbType.String);
            parameters.Add("@Inserted_Updated_By", req.Inserted_Updated_By, DbType.Int32);
            // Table-valued parameter
            var roleDetailsTable = new DataTable();
            roleDetailsTable.Columns.Add("Role_Id", typeof(int));
            roleDetailsTable.Columns.Add("Permission", typeof(string));
            foreach (var role in req.RoleList)
            {
                roleDetailsTable.Rows.Add(role.Id, role.Permission);
            }
            parameters.Add("@Default_Menu_Rights", roleDetailsTable.AsTableValuedParameter("Default_Menu_Rights"));

            //Output parameters
            parameters.Add("@Error_Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Error_Message", dbType: DbType.String, size: 500, direction: ParameterDirection.Output);
            var ResSet= await _dapperHelper.ExecuteStoredProcedureAsync("PRO_INSERT_UPDATE_MENU_MASTER", parameters);
            if (!parameters!.Get<int>("Error_Code").Equals(0))
            {
                Res.responseCode = parameters.Get<int>("Error_Code");
                Res.responseMessage = parameters.Get<string>("Error_Message");
            }
            return Res;
        }
    }
}
