using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Modals
{
    public class ListItem
    {
        public string? LIST_VALUE { get; set; }
        public string? LIST_TEXT { get; set; }
        public bool IS_SELECTED { get; set; } = false;
    }
}
