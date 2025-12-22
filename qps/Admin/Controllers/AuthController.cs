using Domain.Entities.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            // Remove login cookie
            await HttpContext.SignOutAsync(Constants.AuthScheme);

            return Redirect("/Account/Login");
        }
    }
}
