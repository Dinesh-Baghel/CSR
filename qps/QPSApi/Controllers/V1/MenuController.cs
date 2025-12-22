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
    public class MenuController : ControllerBase
    {
        private readonly IMenu _menu;
        public MenuController(IMenu menu)
        {
            _menu = menu;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenus(SelectListReq req)
        {
            var res = await _menu.GetAllMenus(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetMenuList(SelectListReq req)
        {
            var res = await _menu.GetMenuList(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> SetMenu(Menu req)
        {
            var res = await _menu.SetMenu(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
