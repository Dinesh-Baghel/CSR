using Application.Interfaces.V1;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Infrastructure.Services.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QPSApi.Controllers.V1
{
    [Route("api/v1/[Action]")]
    [ApiController]
    public class PPSRequestController : ControllerBase
    {
        private readonly IPPSRequest  _pPSRequest;

        public PPSRequestController(IPPSRequest pPSRequest)
        {
            _pPSRequest = pPSRequest;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePPSRequest(PpsRequest req)
        {
            var res = await _pPSRequest.CreatePPSRequest(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAllPPSRequest(SelectListReq req)
        {
            Type entityType = req.ResponseType!.ToLower() switch
            {
                "ppsrequest" => typeof(PpsRequest),
                "genericarticledetails" => typeof(GenericArticleItem),
                "ppsloghistory" => typeof(PpsLogHistory),
                _ => null
            };

            if (entityType == null)
                return BadRequest("Invalid entity name");

            var method = typeof(IPPSRequest).GetMethod("SelectAllAsync")!.MakeGenericMethod(entityType);
            var task = (Task)method.Invoke(_pPSRequest, new object[] { req });
            await task.ConfigureAwait(false);

            var result = task.GetType().GetProperty("Result")!.GetValue(task);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatusPPSRequest(UpdateStausPpsRequest req)
        {
            var res = await _pPSRequest.UpdateStatusPPSRequest(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

    }
}
