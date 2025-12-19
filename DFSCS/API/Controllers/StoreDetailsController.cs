using Application.Features.Answer.Command;
using Application.Features.Answer.Queries.GetAll;
using Domain.Entities.Model;
using Domain.Entities.Request;
using Domain.Entities.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreDetailsController : ControllerBase
    {
        private readonly InsertAnswer _objanswer;
        private readonly IMediator _mediator;
        public StoreDetailsController(InsertAnswer objanswer, IMediator mediator)
        {
            _mediator = mediator;
            _objanswer = objanswer;
        }

        [HttpPost("getdetails")]
        public async Task<IActionResult> GetDetails([FromBody] StoreDetailsRequest request)
        {
            StoreDetailsResponses res = new StoreDetailsResponses();
            if (string.IsNullOrEmpty(request.storeCode))
            {
                res.responseCode = 12;
                res.responseMessage = "Store Code is mandotary feilds";
                return Ok(res);
            }
            var objresponse = (List<StoreDetails>)await _mediator.Send(new GetStoreDetails(request));
            if (objresponse.Count > 0)
            {
                res.responseCode = 0;
                res.responseMessage = "Success";
                res.storeDetails = objresponse;
            }
            else
            {
                res.responseCode = 10;
                res.responseMessage = "No Data found";
            }
            return Ok(res);
        }
    }
}
