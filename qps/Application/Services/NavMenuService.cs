using Domain.Entities.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NavMenuService
    {
        public event Func<Task>? OnMenuChanged;
        public List<MenusList> MenuList { get; private set; } = new();

        public void UpdateMenuList(List<MenusList> menuList)
        {
            MenuList = menuList;
            NotifyMenuChanged();
        }

        private async void NotifyMenuChanged()
        {
            if (OnMenuChanged != null)
            {
                await OnMenuChanged.Invoke();
            }
        }
    }
}
