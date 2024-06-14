using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;

namespace DAL
{
    public class MenuDao : BaseDao
    {

        public List<MenuItem> GetMenu(int menuId, string itemType)
        {
            string query = GetQueryMenu();

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter ("@MenuId", SqlDbType.Int) {Value = menuId},
        new SqlParameter ("@MenuItemType", SqlDbType.VarChar) { Value = itemType}
            };

            DataTable dataTable = ExecuteSelectQuery(query, parameters);

            List<MenuItem> menuItems = ConvertToList(dataTable);
            return menuItems;
        }

        private string GetQueryMenu()
        {
            return @"
        SELECT 
            MENU_ITEM.item_id,
            MENU_ITEM.name,
            MENU_ITEM.type,
            MENU_ITEM.stock,
            MENU_ITEM.vat,
            MENU_ITEM.price,
            MENU_ITEM.preparation_time
        FROM 
            MENU_ITEM
        JOIN 
            CONTAINING ON MENU_ITEM.item_id = CONTAINING.menu_item
        WHERE 
            CONTAINING.menu_id = @MenuId AND MENU_ITEM.type = @MenuItemType";
        }

        private List<MenuItem> ConvertToList(DataTable table)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            foreach (DataRow row in table.Rows)
            {
                MenuItem menuItem = new MenuItem
                {
                    Id = Convert.ToInt32(row["item_id"]),
                    Name = row["name"].ToString(),
                    Type = row["type"].ToString(),
                    Stock = Convert.ToInt32(row["stock"]),
                    Vat = Convert.ToDecimal(row["vat"]),
                    Price = Convert.ToDecimal(row["price"]),
                    PreparationTime = Convert.ToInt32(row["preparation_time"])
                };

                menuItems.Add(menuItem);
            }

            return menuItems;
        }

        public int GetMenuItemByName(string name)
        {
            string query = "SELECT item_id FROM MENU_ITEM WHERE [name] = @name";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@name", name)
             };
            DataTable dataTable = ExecuteSelectQuery(query, parameters);
            return Convert.ToInt32(dataTable.Rows[0]["item_id"]);
        }

        public int GetPreparationTimeByName(string name)
        {
            string query = "SELECT preparation_time FROM MENU_ITEM WHERE [name] = @name";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@name", name)
             };
            DataTable dataTable = ExecuteSelectQuery(query, parameters);
            return Convert.ToInt32(dataTable.Rows[0]["preparation_time"]);
        }
    }
}
