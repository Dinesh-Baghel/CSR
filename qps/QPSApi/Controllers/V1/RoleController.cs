using Application.Interfaces.V1;
using BSRApplication.Interfaces.V1;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QPSApi.Controllers.V1
{
    [Route("api/v1/[Action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _Role;
        public RoleController(IRole role)
        {
            _Role = role;
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateRole(RoleMaster req)
        {
            var res = await _Role.InsertUpdateRole(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> SelectAllRoles(SelectListReq req)
        {
            var res = await _Role.SelectAllRoles(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> SelectRoleList(SelectListReq req)
        {
            var res = await _Role.SelectAllRoles(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> RoleDetailsOnId(SelectListReq req)
        {
            var res = await _Role.RoleDetailsOnId(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
