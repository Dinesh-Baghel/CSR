using Domain.Entities.Base;
using Domain.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class AppsBsrRes : BaseResponse
    {
        public List<AppsBsr> APPS_BSR { get; set; } = new();
    }
}
