using Application.Common.Interfaces;
using Application.Features.Checklist.Queries;
using AutoMapper;
using Domain.Entities.Common;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ChecklistController : ControllerBase
    {
        private readonly Getchecklist _getchecklist;
        public ChecklistController(Getchecklist getchecklist)
        {
            _getchecklist = getchecklist;
        }
        
        [HttpPost("getdata")]
        public async Task<IActionResult> GetAllList([FromBody] ChecklistRequest request)
        {
            checklistdata res = new checklistdata();
           
           var objresponse = (List<ChecklistResponse>)await _getchecklist.getchecklistdata(request);
            if (objresponse.Count > 0)
            {
                res.responseCode = 0;
                res.responseMessage = "Success";
                res.checklistData = objresponse;
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
