using Application.Interfaces.V1;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QPSApi.Controllers.V1
{
    [Route("api/v1/[Action]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        public readonly IAPI _Iapi;
        public APIController(IAPI iapi)
        {
            _Iapi = iapi;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllApi()
        {
            var AppApiData = await _Iapi.GetAllApi();
            if (AppApiData == null)
            {
                return NotFound();
            }
            return Ok(AppApiData);
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecificApi(SAP_API_DATA_Req req)
        {
            var ApiData = await _Iapi.GetSpecificApiData(req);
            if (ApiData == null)
            {
                return NotFound();
            }
            return Ok(ApiData);
        }
    }
}
