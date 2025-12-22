using Application.Interfaces.V1;
using Domain.Entities.Common;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MenuService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAPICall ApiMasterService;
        private SelectListReq selectListReq = new();

        public MenuService(IHttpContextAccessor httpContextAccessor, IAPICall apiMasterService)
        {
            _httpContextAccessor = httpContextAccessor;
            ApiMasterService = apiMasterService;
        }
        private CopLoginRes? _CopLoginRes;
        public void SetUserLoginData(CopLoginRes loginData)
        {
            _CopLoginRes = loginData;
        }
        public async Task<CopLoginRes?> GetUserLoginData()
        {
            if (_CopLoginRes != null)
                return _CopLoginRes;

            var context = _httpContextAccessor.HttpContext;

            if (context != null && context.User.Identity?.IsAuthenticated == true)
            {
                selectListReq.StrField = context?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                _CopLoginRes = await ApiMasterService.CallingAPI<CopLoginRes, SelectListReq>(AllApiNames.SELECT_LOGGEDIN_USER_ON_EMP_CODE, selectListReq);
            }

            return _CopLoginRes;
        }
        public async Task<List<Menu>> GetMenus()
        {
            var res = await GetUserLoginData();
            return res?.MenuList ?? new List<Menu>();
        }
        public async Task<Menu> GetUserRoleDetails(int Page_Id)
        {
            var res = await GetUserLoginData();
            var UserRoleList = res?.MenuList ?? new List<Menu>();
            var filteredList = UserRoleList.Where(u => u.Id == Page_Id).ToList();
            if (filteredList.Count > 0)
            {
                return filteredList[0];
            }
            else
            {
                return new Menu();
            }
           
        }
        public async Task<Modules> GetModules()
        {
            var res = await GetUserLoginData();
            return res?.Modules!;
        }
    }
}
