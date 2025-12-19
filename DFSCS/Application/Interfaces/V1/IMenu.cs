using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.V1
{
    public interface IMenu
    {
        public Task<List<Menu>> GetAllMenus(SelectListReq req);
        public Task<List<MenusList>> GetMenuList(SelectListReq req);
        public Task<InUpRes> SetMenu(Menu req);
    }
}
