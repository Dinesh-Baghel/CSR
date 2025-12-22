using Application.Interfaces.V1;
using Domain.Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QPSApi.Controllers.V1
{
    [Route("api/v1/[Action]")]
    [ApiController]
    public class SetFlagsController : ControllerBase
    {
        private readonly ISetFlags _ISetFlags;
        public SetFlagsController(ISetFlags setFlags)
        {  
            _ISetFlags = setFlags; 
        }
        [HttpPost]
        public async Task<IActionResult> SetFlags(SetFlagsReq req)
        {
            var res = await _ISetFlags.SetFlags(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
