using Model;
using DAL;
using System;

namespace Service {

    public class MenuItemService {
        private readonly MenuItemDao _menuItemDao;
        public MenuItemService() {
            _menuItemDao = new MenuItemDao();
        }

        public MenuItem GetMenuItemById(int id) {
            return _menuItemDao.GetMenuItemById(id);
        }
    }
}