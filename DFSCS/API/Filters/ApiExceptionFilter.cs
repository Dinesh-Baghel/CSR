using Domain.Entities.Response;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace API.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Unhandled exception occurred.");

            var (status, message) = context.Exception switch
            {
                // Custom application exceptions
                ValidationException ex => (HttpStatusCode.BadRequest, ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToString()! ?? "Validation failed"),
                // Default fallback
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
            };

            var response = new ApiBaseResponse
            {
                responseCode = (int)status,
                responseMessage = message
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)status
            };

            context.ExceptionHandled = true;
        }
    }
}
