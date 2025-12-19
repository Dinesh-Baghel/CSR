using API.Models.Response;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Pass request to next middleware
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Validation failed: {Errors}", ex.Errors);

                var error = ApiErrorResponse.FromContext(context, StatusCodes.Status400BadRequest, ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToString()!, "Validation failed");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(error, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                await context.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                var (status, message, err) = ex switch
                {
                    SecurityTokenExpiredException => (StatusCodes.Status401Unauthorized, "Token expired", "Unauthorized"),
                    SecurityTokenInvalidSignatureException => (StatusCodes.Status401Unauthorized, "Invalid token signature", "Unauthorized"),
                    SecurityTokenInvalidIssuerException => (StatusCodes.Status401Unauthorized, "Invalid token issuer", "Unauthorized"),
                    SecurityTokenInvalidAudienceException => (StatusCodes.Status401Unauthorized, "Invalid token audience", "Unauthorized"),
                    SecurityTokenException => (StatusCodes.Status401Unauthorized, "Invalid JWT token", "Unauthorized"),
                    UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "You do not have permission to access this resource", "Forbidden"),
                    KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found", "Not Found"),
                    FileNotFoundException => (StatusCodes.Status404NotFound, "File not found", "Not Found"),
                    ValidationException validationEx => (StatusCodes.Status400BadRequest, validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToString()!, "Validation failed"),
                    _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred", ex.Message)
                };

                var error = ApiErrorResponse.FromContext(context, status, message, err);
                context.Response.StatusCode = status;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(error, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await context.Response.WriteAsync(json);
            }
        }
    }
}
