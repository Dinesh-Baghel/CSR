using Microsoft.AspNetCore.Http;

using System.Text;


namespace Application.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _accessor;

        public SessionService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void SetString(string key, string value)
        {
            _accessor.HttpContext?.Session.SetString(key, value);
        }

        public string GetString(string key)
        {
            return _accessor.HttpContext?.Session.GetString(key);
        }
    }
}
