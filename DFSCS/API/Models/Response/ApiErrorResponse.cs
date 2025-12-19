using Domain.Entities.Response;

namespace API.Models.Response
{
    public class ApiErrorResponse: ApiBaseResponse
    {
        public string error { get; set; } = string.Empty;
        public static ApiErrorResponse FromContext(HttpContext context, int code, string message, string error)
        {
            return new ApiErrorResponse
            {
                responseCode = code,
                responseMessage = message,
                error=error
            };
        }
    }

    
}
