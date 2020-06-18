using Support.Models;
using Support.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.ViewModel
{
    public class MenuViewModel
    {
        public List<MenuItemModel> MenuItems;
        public MenuViewModel()
        {
            MenuItems = new List<MenuItemModel>()
            {
                new MenuItemModel
                {
                    Id = 0,
                    Title = "Main",                    
                    TargetType = typeof(TransactionPage)
                },
                new MenuItemModel
                {
                    Id = 0,                    
                    Title = "SUPPORT FORM",
                    TargetType = typeof(TransactionWithDetailsPage)
                },
            };
        }
    }
}
