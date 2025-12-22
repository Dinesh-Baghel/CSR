using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
