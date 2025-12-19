
using Application.Features.Option.Queries;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly Getoptionvalues _getvalues;
        public OptionController(Getoptionvalues getvalues)
        {
            _getvalues = getvalues;
        }

        [HttpPost("getvalues")]
        public async Task<IActionResult> GetAllOption()
        {
            OptionValuesdata res = new OptionValuesdata();

            var objresponse = (List<OptionValuesResponses>)await _getvalues.getvalueslistdata();
            if (objresponse.Count > 0)
            {
                res.responseCode = 0;
                res.responseMessage = "Success";
                res.optionData = objresponse;
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
