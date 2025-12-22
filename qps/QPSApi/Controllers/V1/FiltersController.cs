using Application.Interfaces.V1;
using BSRApplication.Interfaces.V1;
using Domain.Entities.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QPSApi.Controllers.V1
{
    [Route("api/v1/[Action]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly IFilters _filters;
        public FiltersController(IFilters filters)
        {
            _filters = filters;
        }
        [HttpPost]
        public async Task<ActionResult> GetDisplayFilters(GetDisplayFiltersReq req)
        {
            var res = await _filters.GetDisplayFilters(req);
            if (res == null) 
            {
                return NotFound();
            }
            return Ok(res);
        }
    }
}
