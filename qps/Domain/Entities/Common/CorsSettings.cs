using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public class CorsSettings
    {
        public string[] AllowBlazorApp { get; set; } = Array.Empty<string>();
    }
}
