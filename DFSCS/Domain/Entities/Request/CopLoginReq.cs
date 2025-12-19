using Domain.Entities.Base;
using Domain.Entities.Common;
using Domain.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Request
{
    public class CopLoginReq : BaseRequest
    {
        public UserDetails UserDetails { get; set; } = new();
    }
}
