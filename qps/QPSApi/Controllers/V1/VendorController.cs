using Application.Interfaces.V1;
using BSRApplication.Interfaces.V1;
using Domain.Entities.Modals;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Collections.Generic;

namespace QPSApi.Controllers.V1
{

    [Route("api/v1/[Action]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendor _vendor;
        
        public VendorController(IVendor vendor)
        {
            _vendor = vendor;
        }


        [HttpPost]
        public async Task<IActionResult> SetVendor(Vendor req)
        {
            var res = await _vendor.SetVendor(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllVendors()
        {
            var res = await _vendor.GetAllVendors();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> GetAddressByVendor(SelectListReq req)
        {
            var res = await _vendor.GetAddressOnVendorCode(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> GetVendorByCode(SelectListReq req)
        {
            var res = await _vendor.GetVendorByID(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> GetVendorWisePOs(SelectListReq req)
        {
            var res = await _vendor.GetAllVendorWisePOs(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> GetPODetails(SelectListReq req)
        {
            var res = await _vendor.GetPODetails(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);

        }


        [HttpPost]
        public async Task<IActionResult> SaveInspectionRequest(POHeader req)
        {
            var res = await _vendor.SaveInspectionRequest(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> GetInspectionByID(SelectListReq req)
        {
            var res = await _vendor.GetInspectionRequestByID(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> GetMasterData(SelectListReq req)
        {
            var res = await _vendor.GetMasterData(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(Vendor req)
        {
            var res = await _vendor.AddAddress(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDOI(UpdateDOIRequest req)
        {
            var res = await _vendor.UpdateDOI(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> AssignInspector(AssignInspectorRequest req)
        {
            var res = await _vendor.AssignInspector(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> SaveInspection(InspectionModel req)
        {
            var res = await _vendor.SaveInspection(req);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(res);
        }

    }
}
