using Application.Interfaces.V1;
using Domain.Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QPSApi.Controllers.V1
{
    [Route("api/v1/[Action]")]
    [ApiController]
    public class AppsBsrController : ControllerBase
    {
        private readonly IAppsBsr _appsBsr;
        public AppsBsrController(IAppsBsr appsBsr)
        {
            _appsBsr = appsBsr;
        }
        [HttpPost]
        public async Task<IActionResult> GetAppsBsr(GetBSRDataReq req)
        {
            var res = await _appsBsr.GetAppsBsr(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
