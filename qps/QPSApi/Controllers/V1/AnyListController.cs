using BSRApplication.Interfaces.V1;
using Domain.Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QPSApi.Controllers.V1
{
    [Route("api/v1/[Action]")]
    [ApiController]
    public class AnyListController : ControllerBase
    {
        private readonly IAnyList _anyList;
        public AnyListController(IAnyList anyList)
        {
            _anyList = anyList;
        }
        [HttpPost]
        public async Task<ActionResult> GetAnyList(GetAnyListReq req)
        {
            var res = await _anyList.GetList(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
