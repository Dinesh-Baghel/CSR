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
    public class UserController : ControllerBase
    {
        private readonly IUser _User;
        public UserController(IUser user)
        {
            _User = user;
        }
        [HttpPost]
        public async Task<IActionResult> CopLogin(CopLoginReq req)
        {
            var res = await _User.CopLogin(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> GetAllUsers(SelectListReq req)
        {
            var res = await _User.GetAllUsers(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> GetUserOnEmpCode(SelectListReq req)
        {
            var res = await _User.GetUserOnEmpCode(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> SetUser(UserDetails req)
        {
            var res = await _User.SetUser(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> GetLoggedInUserOnEmpCode(SelectListReq req)
        {
            var res = await _User.GetLoggedInUserOnEmpCode(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
