using Domain.Entities.Base;
using Domain.Entities.Common;
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
    public interface IRole
    {
        public Task<InUpRes> InsertUpdateRole(RoleMaster req);
        public Task<List<RoleMaster>> SelectAllRoles(SelectListReq req);
        public Task<List<RoleList>> SelectRoleList(SelectListReq req);
        public Task<List<RoleDetails>> RoleDetailsOnId(SelectListReq req);
    }
}
