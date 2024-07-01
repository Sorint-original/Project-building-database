
using Model;
using System;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class MenuItemDao : BaseDao
    {
                public MenuItem GetMenuItemById(int id)
        {
            string query = "SELECT * FROM MENU_ITEM WHERE item_id = @id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            DataTable data = ExecuteSelectQuery(query, sqlParameters);
            return ReadTable(data)[0];
        }

        public List<MenuItem>  ReadTable(DataTable dataTable)
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            foreach (DataRow dr in dataTable.Rows)
            {
                menuItems.Add(new MenuItem{
                    Id = Convert.ToInt32(dr["item_id"]),
                    Name = dr["name"].ToString(),
                    Type = dr["type"].ToString(),
                    Stock = Convert.ToInt32(dr["stock"]),
                    Vat = Convert.ToInt32(dr["vat"]),
                    Price = Convert.ToDecimal(dr["price"]),
                    PreparationTime = Convert.ToInt32(dr["preparation_time"])

                    
                    
                });
            }
            return menuItems;
        }
        
    }
}
