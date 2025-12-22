using Domain.Entities.Base;
using Domain.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Response
{
    public class CopLoginRes : BaseResponse
    {
        public UserDetails UserDetails { get; set; } = new();
        public Modules Modules { get; set; } = new();
        public List<Menu> MenuList { get; set; } = new();
    }
}
