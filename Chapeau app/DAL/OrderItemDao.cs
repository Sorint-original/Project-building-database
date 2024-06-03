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
            query = "SELECT order_id,menu_item,amount,status FROM ORDER_ITEM WHERE order_id = @orderId";

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
                else
                {
                    status = OrderStatus.Served;
                }
                OrderItem item = new OrderItem( (int)dr["order_id"], (int)dr["menu_item"], (int)dr["amount"],status);
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
            string command = "INSERT INTO ORDER_ITEM VALUES (@order_id,@menu_item,@amount,@status)";
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@StudentNumber", item.OrderID);
            sqlParameters[1] = new SqlParameter("@FirstName", item.MenuItemID);
            sqlParameters[2] = new SqlParameter("@SecondName", item.Amount);
            sqlParameters[3] = new SqlParameter("@Phone", item.Status);

            ExecuteEditQuery(command, sqlParameters);
        }

    }

}
