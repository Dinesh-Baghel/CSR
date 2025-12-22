using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyNewEncDec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Middleware
{

    public class StaticTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string ExpectedToken; // Replace with your actual static token

        public StaticTokenMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            var jwtSettings = configuration.GetSection("JwtSettings");
            New_Enc_Dec _Enc_Dec = new();
            ExpectedToken = _Enc_Dec.My_Decode(jwtSettings["SecretKey"]!);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the Authorization header exists
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();

                // Check if the token starts with "Bearer " and retrieve the token
                if (authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();

                    // Validate the token
                    if (token == ExpectedToken)
                    {
                        await _next(context); // Token is valid, proceed
                        return;
                    }
                }
            }

            // Return Unauthorized if token is missing or invalid
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized: Invalid or missing token");
        }
    }
}
