using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class OrderItemDao :BaseDao
    {
        public List<OrderItem> GetAllOrderItemsByOrder(int orderId)
        {
            string query;
            query = "SELECT order_id,menu_item,amount,status,comment FROM ORDER_ITEM WHERE order_id = @orderId";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@orderId", orderId);

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }



        public List<OrderItem> ReadTables(DataTable dataTable)
        {
            List<OrderItem> list = new List<OrderItem> ();

            foreach (DataRow dr in dataTable.Rows)
            {
                string comment;
                try
                {
                    comment = (string)dr["comment"];
                }
                catch
                {
                    comment = null;
                }
                string? comment = dr["comment"] is DBNull ? null : (string)dr["comment"];
                OrderItem item = new OrderItem( (int)dr["order_id"], (int)dr["menu_item"], (int)dr["amount"], status, comment);
                list.Add(item);
            }

            return list;
        }

        OrderStatus GetStatus(string stringStatus)
        {
            if (stringStatus == "Preparing")
            {
                return OrderStatus.Preparing;
            }
            else if (stringStatus == "Ready")
            {
                return OrderStatus.Ready;
            }
            else if (stringStatus == "Served")
            {
                return OrderStatus.Served;
            }
            else
            {
                return OrderStatus.Placed;
            }
        }

        public void DeleteByOrder(int order_id)
        {
            string command = "DELETE FROM ORDER_ITEM WHERE order_id = @Id ";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", order_id);

            ExecuteEditQuery(command, sqlParameters);
        }

        public void AddOrderItem(OrderItem item)
        {
            string command = "INSERT INTO ORDER_ITEM VALUES (@order_id,@menu_item,@amount,@status,@comment)";
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@order_id", item.OrderID);
            sqlParameters[1] = new SqlParameter("@menu_item", item.MenuItemID);
            sqlParameters[2] = new SqlParameter("@amount", item.Amount);
            sqlParameters[3] = new SqlParameter("@status", item.Status.ToString());

            if (item.Comment != null)
            {
                sqlParameters[4] = new SqlParameter("@comment", item.Comment);
            }
            else
            {
                sqlParameters[4] = new SqlParameter("@comment", DBNull.Value);
            }

            ExecuteEditQuery(command, sqlParameters);
        }

        public int GetOrderItemStock(int id)
        {
            string query = "SELECT stock FROM MENU_ITEM WHERE [item_id] = @id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@id", id)
            };
            DataTable data = ExecuteSelectQuery(query, sqlParameters);
            return Convert.ToInt32(data.Rows[0][0]);
        }

        public void RefreshOrderItemStock(int id, int amount)
        {
            string query = "UPDATE MENU_ITEM SET stock = stock - @amount WHERE [item_id] = @id";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@id", id),
                new SqlParameter("@amount", amount)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        public int GetMenuIdByName(string name)
        {
            string query = "SELECT c.menu_id FROM dbo.[MENU_ITEM] mi JOIN dbo.[CONTAINING] c ON mi.item_id = c.menu_item WHERE mi.name = @name;";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@name", name)
            };
            DataTable data = ExecuteSelectQuery(query, sqlParameters);
            return Convert.ToInt32(data.Rows[0][0]);
        }


    }

}
