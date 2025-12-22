using Domain.Entities.Base;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSRApplication.Interfaces.V1
{
    public interface IRole
    {
        public Task<InUpRes> InsertUpdateRole(RoleMaster req);
        public Task<SelectAllRoles> SelectAllRoles(SelectListReq req);
        public Task<List<RoleList>> SelectRoleList(SelectListReq req);
        public Task<RoleDetailsOnIdRes> RoleDetailsOnId(SelectListReq req);
    }
}
