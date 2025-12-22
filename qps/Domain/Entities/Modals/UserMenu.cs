using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class UserMenu
    {
        public List<Menu> menu { get; set; } = new();
        public Modules modules { get; set; } = new();
    }
}
