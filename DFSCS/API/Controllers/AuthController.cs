
using Application.Common.Interfaces;
using Domain.Entities.Common;
using Domain.Entities.Request;
using Domain.Entities.Response;
using Infrastructure.Security;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _config;
        private readonly IApiCall iAPICall;
        private readonly ApiData apiData;
        public AuthController(IJwtService jwtService, IApiCall iAPICall, ApiData apiData, IConfiguration config)
        {
            _jwtService = jwtService;
            this.iAPICall = iAPICall;
            this.apiData = apiData;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            LoginResponse loginResponse1 = new LoginResponse();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var hostName = "";
            if (ipAddress == "::1")
            {
                ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
                hostName = Dns.GetHostEntry(Dns.GetHostName()).HostName.Split('.')[0];
            }
            else
            {
                ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
                hostName = Dns.GetHostEntry(Dns.GetHostName()).HostName.Split('.')[0];
            }
            var expiryInSeconds = _config["Jwt:DurationInSeconds"];
            var apidata = apiData;
            var loginResponse = await iAPICall.CallApi<HrpLoginRes, LoginRequest>(request!, apidata);
            if (loginResponse.responseCode == 0 && loginResponse!.responseMessage == "Success" && loginResponse.employeeList?.Count > 0)
            {
                var token = _jwtService.GenerateToken(loginResponse.employeeList[0].empCode, loginResponse.employeeList[0].empName, "Admin");
                loginResponse1.userDetails = loginResponse.employeeList;
                loginResponse1.accessToken = token;
                loginResponse1.expiresInSeconds = int.Parse(expiryInSeconds!);// _tokenSettings.DurationInMinutes;
                return Ok(loginResponse1);
            }
            else
            {
                //ShowAlert(loginResponse.responseMessage!);
            }

            loginResponse1.responseCode = 1;
            loginResponse1.responseMessage = "Invalid username or password";
            return Unauthorized(loginResponse1);

        }
    }

}
