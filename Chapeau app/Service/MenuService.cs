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

        public string GetMenuItemById(int id)
        {
            return menuDao.GetMenuItemById(id);
        }

        public MenuItem GetMenuItemByID(int Id)
        {
            return menuDao.GetMenuItemByID(Id);
        }

        public int GetPreparationTimeByName(string name)
        { 
            return menuDao.GetPreparationTimeByName(name);
        }
    }
}
