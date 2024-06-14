using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class MenuService
    {
        MenuDao menuDao;

        public MenuService() 
        {
            menuDao = new MenuDao();
        }

        public List<MenuItem> GetPartMenu(int menuId, string itemType)
        { 
            return menuDao.GetMenu(menuId, itemType);
        }

        public int GetMenuItemByName(string name)
        {
            return menuDao.GetMenuItemByName(name);
        }

        public int GetPreparationTimeByName(string name)
        { 
            return menuDao.GetPreparationTimeByName(name);
        }
    }
}
