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

                OrderStatus status = new OrderStatus();
                string tablestatus = (string)dr["status"];
                if(tablestatus == "Preparing")
                {
                    status = OrderStatus.Preparing;
                }
                else if(tablestatus =="Ready")
                {
                    status = OrderStatus.Ready;
                }
                else if (tablestatus == "Served")
                {
                    status = OrderStatus.Served;
                }
                else
                {
                    status = OrderStatus.Placed;
                }
                OrderItem item = new OrderItem( (int)dr["order_id"], (int)dr["menu_item"], (int)dr["amount"], status, (string)dr["comment"]);
                list.Add(item);
            }

            return list;
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
            sqlParameters[4] = new SqlParameter("@comment", item.Comment);

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

    
    }

}
