using Domain.Entities.Base;
using Domain.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class GetUserOnEmpCodeRes : BaseResponse
    {
        public UserDetails userDetails { get; set; } = new();
    }
}
