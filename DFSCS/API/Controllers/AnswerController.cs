using Application.Features.Answer.Command;
using Application.Features.Answer.Queries.GetAll;
using Application.Features.Checklist.Queries;
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
    public class AnswerController : ControllerBase
    {
        private readonly InsertAnswer _objanswer;
        private readonly IMediator _mediator;
        public AnswerController(InsertAnswer objanswer,IMediator mediator)
        {
          _mediator = mediator;
            _objanswer = objanswer;
        }
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitReply([FromBody] AnswerRequest request)
        {
            var objresponse = (ApiBaseResponse)await _objanswer.submitanswer(request);
            return Ok(objresponse);

        }

        [HttpPost("fetch")]
        public async Task<IActionResult> FetchReply([FromBody] AnswerListRequest request)
        {
            AnswerList res = new AnswerList();
            var objresponse = (List<Answerdata>)await _mediator.Send(new GetAllAnswersQuery(request));
            if (objresponse.Count > 0)
            {
                res.responseCode = 0;
                res.responseMessage = "Success";
                res.answerlistData = objresponse;
            }
            else
            {
                res.responseCode = 0;
                res.responseMessage = "No Data found";
            }
            return Ok(res);
        }

        [HttpPost("detail")]
        public async Task<IActionResult> FetchDetailReply([FromBody] AnswerDetailsRequest request)
        {
            AnswerDetails res = new AnswerDetails();
            var objresponse = (List<AnswerDetailsitem>)await _mediator.Send(new GetAnswersDetails(request));
            if (objresponse!=null)
            {
                res.responseCode = 0;
                res.responseMessage = "Success";
                res.answerDetails = objresponse;
            }
            else
            {
                res.responseCode = 0;
                res.responseMessage = "No Data found";
            }
            return Ok(res);
        }
    }
}
